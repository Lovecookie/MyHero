

using Shared.Featrues.Auth;

namespace Shared.Featrues.Extensions;
public static class ClaimsPrincipalExtensions
{
	public static Int64 GetUxt(this ClaimsPrincipal principal)
	{
		var claimUxt = principal.FindFirstValue(CustomClaimType.Uxt);
		if(claimUxt == null)
		{
			return 0;
		}

		return Convert.ToInt64(claimUxt);
	}

	public static bool IsAuthenticated(this ClaimsPrincipal principal)
	{
		return principal.Identity?.IsAuthenticated == true;
	}
}
