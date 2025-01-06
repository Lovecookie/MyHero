
namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class AccessJwtCommand(long userUID, string email) : IRequest<TOutcome<TokenInfo>>
{
    public long UserUID { get; init; } = userUID;

    public string Email { get; init; } = email;
}

/// <summary>
/// 
/// </summary>
public class AccessJwtCommandHandler : IRequestHandler<AccessJwtCommand, TOutcome<TokenInfo>>
{
    private readonly IUserAuthJwtRepository _userAuthJwtRepository;
    private readonly JwtFields _jwtFields;
    
    public AccessJwtCommandHandler(IUserAuthJwtRepository userAuthJwtRepository, IOptions<JwtFields> jwtFields)
    {
        _userAuthJwtRepository = userAuthJwtRepository;
        _jwtFields = jwtFields.Value;
    }

    public async Task<TOutcome<TokenInfo>> Handle(AccessJwtCommand request, CancellationToken cancellationToken)
    {
        var accessToken = await TokenHelper.GenerateAccessJwt(request.UserUID, _jwtFields);
        var refreshToken = await TokenHelper.GenerateRefreshJwt(request.UserUID, _jwtFields);

        UserAuthJwt userAuthJwt = new UserAuthJwt
        {
            UserUID = request.UserUID,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };

        var bResult = await _userAuthJwtRepository.Add(userAuthJwt);
        if (!bResult)
        {
            return TOutcome.Error<TokenInfo>("Error updating refresh token");
        }

        bResult = await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        if (!bResult)
        {
            return TOutcome.Error<TokenInfo>("Error updating refresh token");
        }

        return TOutcome.Success(new TokenInfo(accessToken, refreshToken));
    }
}