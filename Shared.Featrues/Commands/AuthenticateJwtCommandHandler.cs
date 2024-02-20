
namespace Shared.Features.Commands;

public class AuthenticateJwtCommandHandler : IRequestHandler<AuthenticateJwtCommand, TOptional<string>>
{
	private readonly IConfiguration _configuration;	

	public AuthenticateJwtCommandHandler( IConfiguration configuration )
	{
		_configuration = configuration;
	}

	public async Task<TOptional<string>> Handle(AuthenticateJwtCommand request, CancellationToken cancellationToken)
	{
		var jwtSecretKey = _configuration["Jwt:SecretKey"];
		if (jwtSecretKey == null)
		{
			return TOptional.Error<string>("Secret key not found");
		}

		var jwtIssuer = _configuration["Jwt:Issuer"];
		if (jwtIssuer == null)
		{
			return TOptional.Error<string>("Issuer not found");
		}

		var jwtAudience = _configuration["Jwt:Audience"];
		if (jwtAudience == null)
		{
			return TOptional.Error<string>("Audience not found");
		}

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));		
		var tokenOptions = new JwtSecurityToken(
			issuer: jwtIssuer,
			audience: jwtAudience,
			claims: new List<Claim>(),
			expires: DateTime.Now.AddMinutes(5),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.RsaSha256)
			);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

		return TOptional.To(tokenString);
	}
}