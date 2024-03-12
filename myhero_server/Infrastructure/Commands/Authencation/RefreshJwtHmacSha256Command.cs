

namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public record JwtHmacSha256Command(SharedLoginRequest Request) : IRequest<TOptional<string>>
{   
}

/// <summary>
/// 
/// </summary>
public class JwtHmacSha256CommandHandler : IRequestHandler<JwtHmacSha256Command, TOptional<string>>
{
	private readonly JwtFields _jwtFields;

	public JwtHmacSha256CommandHandler(IOptions<JwtFields> jwtFields)
	{
		_jwtFields = jwtFields.Value;
	}

	public async Task<TOptional<TokenInfo>> Handle(JwtHmacSha256Command request, CancellationToken cancellationToken)
	{	
		var accessToken = TokenHelper.GenerateAccessJwt(request.Request.Email, _jwtFields);
		var refreshToken = TokenHelper.GenerateRefreshJwt(request.Request.Email, _jwtFields);

		var tokenInfo = new TokenInfo(accessToken, refreshToken);

		var encryptAccess = await AesEncryption.EncryptAsync(accessToken);
		var encryptRefresh = await AesEncryption.EncryptAsync(refreshToken);

		return TOptional.To(tokenInfo);
	}
}