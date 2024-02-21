using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.Account;


public static class SearchApi
{
    public static WebApplication MapSearchApis(this WebApplication app)
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

        Serilog.Log.Information("[Success] SearchApi mapped");

        return app;
    }

    public static async Task<IResult> SearchUser(
        [FromBody] SearchUserRequest searchUserRequest,
        [AsParameters] AccountServices services
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