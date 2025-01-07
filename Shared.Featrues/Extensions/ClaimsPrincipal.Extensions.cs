

using Shared.Featrues.Algorithm;
using Shared.Features.Auth;

namespace Shared.Features.Extensions;
public static class ClaimsPrincipalExtensions
{
	public static bool IsValidClaims(this ClaimsPrincipal principal)
	{
		if( !principal.IsAuthenticated() )
		{
			return false;
		}

		return true;
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

	public static async Task<TOutcome<Int64>> TryDecryptUID(this ClaimsPrincipal principal)
	{
		var claimUxt = principal.FindFirstValue(CustomClaimType.Uxt);
		if(claimUxt == null)
		{
			return TOutcome.Err<Int64>("Not authenticated.");
		}

		var decryptedUID = await AesWrapper.DecryptAsInt64(claimUxt);
		if(!decryptedUID.HasValue)
		{
			return TOutcome.Err<Int64>("Invalid ID.");
		}

		return TOutcome.Ok(decryptedUID.Value);
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

	private static bool IsAuthenticated(this ClaimsPrincipal principal)
	{
		return principal.Identity?.IsAuthenticated == true;
	}
}
