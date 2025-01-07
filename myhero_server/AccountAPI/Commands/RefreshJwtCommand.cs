
namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class RefreshJwtCommand(ClaimsPrincipal principal) : IRequest<TOutcome<string>>
{
    public ClaimsPrincipal Principal { get; init; } = principal;
}

/// <summary>
/// 
/// </summary>
public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, TOutcome<string>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IUserAuthJwtRepository _userAuthJwtRepository;
    private readonly JwtFields _jwtFields;

    public RefreshJwtCommandHandler(IUserBasicRepository userBasicRepository, IUserAuthJwtRepository userAuthJwtRepository, IOptions<JwtFields> jwtFields)
    {
        _userBasicRepository = userBasicRepository;
        _userAuthJwtRepository = userAuthJwtRepository;
        _jwtFields = jwtFields.Value;
    }

    public async Task<TOutcome<string>> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
    {
        var principal = request.Principal;
        if (!principal.IsRefreshType())
        {
            return TOutcome.Err<string>("Unauthorized");
        }

        var uidOpt = await principal.TryDecryptUID();
        if (!uidOpt.Success)
        {
            return TOutcome.Err<string>("Unauthorized");
        }

        var userUID = uidOpt.Value;
        if (userUID == 0)
        {
            return TOutcome.Err<string>("Unauthorized");
        }

        var userBasicOpt = await _userBasicRepository.Find(userUID);
        if (!userBasicOpt.Success)
        {
            return TOutcome.Err<string>("User not found");
        }

        var userBasic = userBasicOpt.Value!;

        var accessToken = await TokenHelper.GenerateAccessJwt(userBasic.UserUID, _jwtFields);

        var bResult = await _userAuthJwtRepository.UpdateAccessToken(userBasic.UserUID, accessToken);
        if (!bResult)
        {
            return TOutcome.Err<string>("Error updating refresh token");
        }

        await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return TOutcome.Ok(accessToken);
    }
}