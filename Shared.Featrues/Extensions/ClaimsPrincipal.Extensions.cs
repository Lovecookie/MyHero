

using Shared.Featrues.Algorithm;
using Shared.Features.Auth;

namespace Shared.Features.Extensions;
public static class ClaimsPrincipalExtensions
{
	public static bool IsAuthenticated(this ClaimsPrincipal principal)
	{
		return principal.Identity?.IsAuthenticated == true;
	}

	public static string? GetEncryptedUID(this ClaimsPrincipal principal)
	{
		var claimUxt = principal.FindFirstValue(CustomClaimType.Uxt);
		if(claimUxt == null)
		{
			return null;
		}

		return claimUxt;
	}

	public static async Task<Int64?> DecryptUID(this ClaimsPrincipal principal)
	{
		var claimUxt = principal.FindFirstValue(CustomClaimType.Uxt);
		if(claimUxt == null)
		{
			return null;
		}

		var decryptedUID = await AesWrapper.DecryptAsInt64(claimUxt);
		if(decryptedUID == 0)
		{
			return null;
		}

		return decryptedUID;
	}


	public static bool IsAccessType(this ClaimsPrincipal principal)
	{
		var claimAccessType = principal.FindFirstValue(CustomClaimType.TokenType);
		if(claimAccessType == null)
		{
			return false;
		}

		return claimAccessType == CustomTokenType.Access;
	}

	public static bool IsRefreshType(this ClaimsPrincipal principal)
	{
		var claimAccessType = principal.FindFirstValue(CustomClaimType.TokenType);
		if(claimAccessType == null)
		{
			return false;
		}

		return claimAccessType == CustomTokenType.Refresh;
	}
}
