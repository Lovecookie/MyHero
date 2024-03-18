using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.Account;


public static class AccountApi
{
    public static WebApplication MapAccountApis(this WebApplication app)
    {
        var apiName = "account";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        //root.MapPost("/login", LoginUser)
        //    .ProducesProblem(StatusCodes.Status500InternalServerError)
        //    .WithSummary("Login User")
        //    .WithDescription("\n POST /login");
        root.MapPost("/my-profile", MyProfile)
			.WithSummary("My Profile")
			.WithDescription("\n POST /my-profile")
			.RequireAuthorization();

        Serilog.Log.Information("[Success] AccountApis mapped");        

        return app;
    }

	public static async Task<IResult> MyProfile(ClaimsPrincipal principal)
	{
		await Task.CompletedTask.WaitAsync(TimeSpan.Zero);

		var userUIDClaim = principal.FindFirstValue(CustomClaimType.Uxt);
		if (userUIDClaim == null)
		{
			return ToClientResults.Error("Unauthorized");
		}

		return Results.Ok();
	}

}