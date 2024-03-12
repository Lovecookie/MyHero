using Util.Infrastructure.Jwt;

namespace myhero_dotnet.Infrastructure;



/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public record RefreshJwtHmacSha256Command(SharedLoginRequest Request) : IRequest<TOptional<string>>
{   
}

/// <summary>
/// 
/// </summary>
public class RefreshJwtHmacSha256CommandHandler : IRequestHandler<RefreshJwtHmacSha256Command, TOptional<string>>
{
	private readonly JwtFields _jwtFields;

	public RefreshJwtHmacSha256CommandHandler(IOptions<JwtFields> jwtFields)
	{
		_jwtFields = jwtFields.Value;
	}

	public async Task<TOptional<string>> Handle(RefreshJwtHmacSha256Command request, CancellationToken cancellationToken)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, request.Request.Email)
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtFields.Secret));

		var tokenOptions = new JwtSecurityToken(
			issuer: _jwtFields.Issuer,
			audience: _jwtFields.Audience,
			claims: claims,
			expires: DateTime.Now.AddDays(7),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
			);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

		return TOptional.To(tokenString);
	}
}