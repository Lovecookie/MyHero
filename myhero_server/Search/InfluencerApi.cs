using myhero_dotnet.Account.Requests;
using myhero_dotnet.Account.Services;
using myhero_dotnet.Infrastructure.Commands;
using myhero_dotnet.Infrastructure.Commands.User;
using myhero_dotnet.Infrastructure.Features;


namespace myhero_dotnet.Search;


public static class SearchApi
{
    public static WebApplication MapInfluencerApis(this WebApplication app)
    {
        var apiName = "search";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapGet("/user", SearchUser)
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

        await services.Mediator.Send(createUserCommand);

        return Results.Ok();
    }
}