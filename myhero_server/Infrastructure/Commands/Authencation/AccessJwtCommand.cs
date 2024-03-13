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
		var accessToken = TokenHelper.GenerateAccessJwt(request.UserUID, request.Email, _jwtFields);
		var refreshToken = TokenHelper.GenerateRefreshJwt(request.UserUID, request.Email, _jwtFields);

		var encryptAccess = await AesEncryption.EncryptAsync(accessToken);
		var encryptRefresh = await AesEncryption.EncryptAsync(refreshToken);

		UserAuthJwt userAuthJwt = new UserAuthJwt
		{
			UserUID = request.UserUID,
			AccessToken = encryptAccess,
			RefreshToken = encryptRefresh,
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

		return TOptional.To(new TokenInfo(accessToken, refreshToken));
	}
}