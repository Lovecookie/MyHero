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

        Serilog.Log.Information("[Success] AccountApis mapped");        

        return app;
    }

}