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

        root.MapPost("/login", LoginUser)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Login User")
            .WithDescription("\n POST /login");

        Serilog.Log.Information("[Success] AccountApis mapped");        

        return app;
    }


    public static async Task<IResult> LoginUser(
        [FromBody] LoginUserRequest loginUserRequest,
        [AsParameters] AccountServices services)
    {
        var loginUserCommand = services.Mapper.Map<LoginUserCommand>(loginUserRequest);

		//var result = await services.LoginUser(user);

		return await Task.FromResult(Results.Ok());
	}   
}