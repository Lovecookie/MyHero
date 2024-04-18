using myhero_dotnet.Infrastructure;
using Shared.Featrues.Algorithm;
using Shared.Featrues.Requests;


namespace myhero_dotnet.ContentsAPI;


public static class FeedAPI
{
    public static WebApplication MapFeedAPI(this WebApplication app)
    {
        var apiName = "feed";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapGet("/{uid:minlength(2)}/", GetFeed)
            .WithSummary("Get Feed")
            .WithDescription("\n GET /{uid:minlength(2)}");

        //root.MapGet("/user/by/{name:minlength(2)}", SearchUserByName)
        //    .WithSummary("Search User")
        //    .WithDescription("\n GET /user/by/{name:minlength(2)}");

        Log.Information("[Success] FeedAPI mapped");

        return app;
    }

    public static async Task<IResult> GetFeed(
        [AsParameters] PaginationRequest request,
		[AsParameters] ContentsServices services,
        string uid)
    {
        var pageIndex = request.PageIndex;
        var pageSize = request.PageSize;

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
}