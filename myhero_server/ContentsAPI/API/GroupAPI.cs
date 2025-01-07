using myhero_dotnet.Infrastructure;
using Shared.Featrues.Algorithm;


namespace myhero_dotnet.ContentsAPI;


public static class GroupAPI
{
    public static WebApplication MapGroupAPI(this WebApplication app)
    {
        var apiName = "group";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapGet("/user/{uid:minlength(2)}", SearchGroup)
            .WithSummary("Get User")
            .WithDescription("\n GET /user/{uid:minlength(2)}");        

        Log.Information("[Success] SearchAPI mapped");

        return app;
    }

    public static async Task<IResult> SearchGroup(
        [AsParameters] ContentsServices services,
        string uid)
    {
        var decryptedUID = await AesWrapper.DecryptAsInt64(uid);
        if(!decryptedUID.HasValue)
        {
			return ToClientResults.Error("Invalid ID.");
		}

        var opt = await services.Mediator.Send(new SearchGroupCommand(decryptedUID.Value));
		if (!opt.Success)
        {
			return ToClientResults.Error("Not found.");
		}

		return ToClientResults.Ok(opt.Value!);
    }   
}