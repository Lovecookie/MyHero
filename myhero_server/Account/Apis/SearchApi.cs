using Microsoft.AspNetCore.Http.Metadata;
using myhero_dotnet.Account.Requests;
using myhero_dotnet.Account.Services;
using myhero_dotnet.Infrastructure.Commands;
using myhero_dotnet.Infrastructure.Commands.User;
using myhero_dotnet.Infrastructure.Features;
using myhero_dotnet.Infrastructure.StatusResult;


namespace myhero_dotnet.Account.Apis;


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