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

        root.MapPost("/user", SearchUser)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Search User")
            .WithDescription("\n GET /user");

        Log.Information("[Success] SearchApi mapped");

        return app;
    }

    public static async Task<IResult> SearchUser(
        [FromBody] SearchUserRequest searchUserRequest,
        [AsParameters] ContentsServices services
        )
    {
        var createUserCommand = services.Mapper.Map<SearchUserCommand>(searchUserRequest);

        var opt = await services.Mediator.Send(createUserCommand);
        if (!opt.HasValue)
        {
            return ToClientResults.Error("Not found.");
        }

        return ToClientResults.Ok(opt.Value!);
    }
}