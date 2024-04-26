using myhero_dotnet.Infrastructure;
using Shared.Featrues.Algorithm;


namespace myhero_dotnet.ContentsAPI;


public static class MessageAPI
{
    public static WebApplication MapMessageAPI(this WebApplication app)
    {
        var apiName = "msg";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapPost("/rand", RandomSendMessage)
            .WithSummary("Random Send Message")
            .WithDescription("\n POST /rand");

        root.MapGet("/user/by/{name:minlength(2)}", SearchUserByName)
            .WithSummary("Search User")
            .WithDescription("\n GET /user/by/{name:minlength(2)}");

        Log.Information("[Success MessageAPI mapped");

        return app;
    }

    public static async Task<IResult> RandomSendMessage(
        [AsParameters] ContentsServices services,
        string uid)
    {
        var decryptedUID = await AesWrapper.DecryptAsInt64(uid);
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

    public static async Task<IResult> SearchUserByName(
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