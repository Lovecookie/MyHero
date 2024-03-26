

using Shared.Features.Crypt;

namespace Shared.Features.Auth;

public static class TokenHelper
{

	public static async Task<string> GenerateAccessJwt(Int64 userUID, JwtFields fields)
	{
		var encryptedUID = await AesEncryption.EncryptAsString(userUID.ToString());

		var claims = new List<Claim>
		{
			new Claim(CustomClaimType.TokenType, CustomTokenType.Access),
			new Claim(CustomClaimType.Uxt, encryptedUID)
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fields.Secret));

		var tokenOptions = new JwtSecurityToken(
			issuer: fields.Issuer,
			audience: fields.Audience,
		claims: claims,
			expires: DateTime.Now.AddHours(fields.AccessExpiresHours),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
			);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}

	public static async Task<string> GenerateRefreshJwt(Int64 userUID, JwtFields fields)
	{
		var encryptedUID = await AesEncryption.EncryptAsString(userUID.ToString());

		var claims = new List<Claim>
		{
			new Claim(CustomClaimType.TokenType, CustomTokenType.Refresh),
			new Claim(CustomClaimType.Uxt, encryptedUID)			
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fields.Secret));

		var tokenOptions = new JwtSecurityToken(
			issuer: fields.Issuer,
			audience: fields.Audience,
		claims: claims,
			expires: DateTime.Now.AddDays(fields.RefreshExpiresDays),
			signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
			);

		return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
	}
}
