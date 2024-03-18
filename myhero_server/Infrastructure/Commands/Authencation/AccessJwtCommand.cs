using myhero_dotnet.Account;


namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class AccessJwtCommand(Int64 userUID, string email ) : IRequest<TOptional<TokenInfo>>
{
	public Int64 UserUID { get; init; } = userUID;

	public string Email { get; init; } = email;
}

/// <summary>
/// 
/// </summary>
public class AccessJwtCommandHandler : IRequestHandler<AccessJwtCommand, TOptional<TokenInfo>>
{	
	private readonly IUserAuthJwtRepository _userAuthJwtRepository;
	private readonly JwtFields _jwtFields;

	public AccessJwtCommandHandler(IUserAuthJwtRepository userAuthJwtRepository, IOptions<JwtFields> jwtFields)
	{		
		_userAuthJwtRepository = userAuthJwtRepository;
		_jwtFields = jwtFields.Value;
	}

	public async Task<TOptional<TokenInfo>> Handle(AccessJwtCommand request, CancellationToken cancellationToken)
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
			return TOptional.Error<TokenInfo>("Error updating refresh token");
		}

		bResult = await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		if (!bResult)
		{
			return TOptional.Error<TokenInfo>("Error updating refresh token");
		}

		return TOptional.Success(new TokenInfo(accessToken, refreshToken));
	}
}