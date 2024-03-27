using myhero_dotnet.Infrastructure;


namespace myhero_dotnet.ContentsAPI;


public static class SearchApi
{
    public static WebApplication MapSearchAPI(this WebApplication app)
    {
        var apiName = "search";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapGet("/user/{uid:minlength(2)}", GetUser)
            .WithSummary("Get User")
            .WithDescription("\n GET /user/{uid:minlength(2)}");

        root.MapGet("/user/by/{name:minlength(2)}", SearchUser)            
            .WithSummary("Search User")
            .WithDescription("\n GET /user/by/{name:minlength(2)}");

        Log.Information("[Success] SearchAPI mapped");

        return app;
    }

    public static async Task<IResult> GetUser(
        [AsParameters] ContentsServices services,
        string uid)
    {
        var decryptedUID = await AesEncryption.DecryptAsInt64(uid);
        if(!decryptedUID.HasValue)
        {
			return ToClientResults.Error("Invalid ID.");
		}

        var opt = await services.Mediator.Send(new SearchUserCommand(decryptedUID.Value));
		if (!opt.HasValue)
        {
			return ToClientResults.Error("Not found.");
		}

		return ToClientResults.Ok(opt.Value!);
    }

    public static async Task<IResult> SearchUser(
        [AsParameters] ContentsServices services,
        string name
        )
    {
        var opt = await services.Mediator.Send(new SearchUserByStringCommand(name, EUserSearchType.Name));
        if (!opt.HasValue)
        {
            return ToClientResults.Error("Not found.");
        }

        return ToClientResults.Ok(opt.Value!);
    }
}