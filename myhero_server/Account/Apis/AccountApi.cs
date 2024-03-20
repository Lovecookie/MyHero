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
        
        root.MapGet("/profile", Profile)
			.WithSummary("My Profile")
			.WithDescription("\n GET /profile")
			.RequireAuthorization();

        Serilog.Log.Information("[Success] AccountApis mapped");        

        return app;
    }

	public static async Task<IResult> Profile(
		ClaimsPrincipal principal,
		[AsParameters] AccountServices services
		)
	{
		var responseOpt = await services.Mediator.Send(new MyProfileCommand(principal));
		if (!responseOpt.HasValue)
		{
			return ToClientResults.Error(responseOpt.Message);
		}

		return ToClientResults.Ok(responseOpt.Value!);
	}

}