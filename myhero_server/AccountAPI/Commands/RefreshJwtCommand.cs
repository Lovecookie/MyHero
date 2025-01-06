
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
            return TOutcome.Error<string>("Unauthorized");
        }

        var uidOpt = await principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOutcome.Error<string>("Unauthorized");
        }

        var userUID = uidOpt.Value;
        if (userUID == 0)
        {
            return TOutcome.Error<string>("Unauthorized");
        }

        var userBasicOpt = await _userBasicRepository.Find(userUID);
        if (!userBasicOpt.HasValue)
        {
            return TOutcome.Error<string>("User not found");
        }

        var userBasic = userBasicOpt.Value!;

        var accessToken = await TokenHelper.GenerateAccessJwt(userBasic.UserUID, _jwtFields);

        var bResult = await _userAuthJwtRepository.UpdateAccessToken(userBasic.UserUID, accessToken);
        if (!bResult)
        {
            return TOutcome.Error<string>("Error updating refresh token");
        }

        await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return TOutcome.Success(accessToken);
    }
}