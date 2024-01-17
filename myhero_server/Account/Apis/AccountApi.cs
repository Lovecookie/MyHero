using myhero_dotnet.Account.Requests;
using myhero_dotnet.Account.Services;
using myhero_dotnet.Infrastructure.Commands;
using myhero_dotnet.Infrastructure.Commands.User;
using myhero_dotnet.Infrastructure.Features;


namespace myhero_dotnet.Account.Apis;


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

		root.MapPost("/create", CreateUser)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create User")
            .WithDescription("\n POST /create");

        root.MapPost("/login", LoginUser)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Login User")
            .WithDescription("\n POST /login");

        Serilog.Log.Information("[Success] AccountApis mapped");        

        return app;
    }

    public static async Task<IResult> Heartbeat(
        [AsParameters] AccountServices services )
    {
        var heartbeatCommand = new HeartbeatCommand
        {
            HeartBeat = "1"
        };

        var heatbeat = await services.Mediator.Send(heartbeatCommand);

        return Results.Ok(heatbeat!);
    }

    public static async Task<IResult> CreateUser(
        [FromBody] CreateUserRequest createUserRequest,
        [AsParameters] AccountServices services
        )
    {
        var createUserCommand = services.Mapper.Map<CreateUserCommand>(createUserRequest);

        await services.Mediator.Send(createUserCommand);        

        return Results.Ok();
    }

    public static async Task<IResult> LoginUser(
        [FromBody] LoginUserRequest loginUserRequest,
        [AsParameters] AccountServices services)
    {
		//var result = await services.LoginUser(user);

		return await Task.FromResult(Results.Ok());
	}   
}