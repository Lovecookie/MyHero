
namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class RefreshJwtCommand(ClaimsPrincipal principal) : IRequest<TOptional<string>>
{
    public ClaimsPrincipal Principal { get; init; } = principal;
}

/// <summary>
/// 
/// </summary>
public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, TOptional<string>>
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

    public async Task<TOptional<string>> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
    {
        var principal = request.Principal;
        if (principal.IsRefreshType())
        {
            return TOptional.Error<string>("Unauthorized");
        }

        var uidOpt = await principal.DecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOptional.Error<string>("Unauthorized");
        }

        var userUID = uidOpt.Value;
        if (userUID == 0)
        {
            return TOptional.Error<string>("Unauthorized");
        }

        var userBasicOpt = await _userBasicRepository.Find(userUID);
        if (!userBasicOpt.HasValue)
        {
            return TOptional.Error<string>("User not found");
        }

        var userBasic = userBasicOpt.Value!;

        var accessToken = await TokenHelper.GenerateAccessJwt(userBasic.UserUID, _jwtFields);

        var bResult = await _userAuthJwtRepository.UpdateAccessToken(userBasic.UserUID, accessToken);
        if (!bResult)
        {
            return TOptional.Error<string>("Error updating refresh token");
        }

        await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return TOptional.Success(accessToken);
    }
}