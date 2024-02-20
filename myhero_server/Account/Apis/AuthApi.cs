using myhero_dotnet.Account.Requests;
using myhero_dotnet.Account.Services;
using myhero_dotnet.Infrastructure.Commands;
using myhero_dotnet.Infrastructure.Commands.User;
using myhero_dotnet.Infrastructure.Features;
using myhero_dotnet.Infrastructure.StatusResult;


namespace myhero_dotnet.Account.Apis;


public static class AuthApi
{
	public static WebApplication MapAuthApis(this WebApplication app)
	{
		var apiName = "auth";
		var apiUri = ConstantVersion.URL(apiName);

		var root = app.MapGroup(apiUri)
			.WithGroupName(ConstantVersion.GlobalVersionByLower)
			.WithTags(apiName)
			.WithOpenApi();

		root.MapPost("/login", Login)
			.ProducesProblem(StatusCodes.Status500InternalServerError)
			.WithSummary("Login User")
			.WithDescription("\n POST /login");

		root.MapPost("/refresh", Refresh)
			.ProducesProblem(StatusCodes.Status500InternalServerError)
			.WithSummary("Refresh Token")
			.WithDescription("\n POST /refresh");

		Serilog.Log.Information("[Success] AuthApis mapped");

		return app;
	}


	public static async Task<IResult> Login(
		[FromBody] LoginUserRequest loginUserRequest,
		[AsParameters] AccountServices services)
	{
		var loginUserCommand = services.Mapper.Map<LoginUserCommand>(loginUserRequest);

		//var result = await services.LoginUser(user);

		return await Task.FromResult(Results.Ok());
	}

	public static async Task<IResult> Refresh(
				[FromBody] RefreshTokenRequest refreshTokenRequest,
						[AsParameters] AccountServices services)
	{
		var refreshTokenCommand = services.Mapper.Map<RefreshTokenCommand>(refreshTokenRequest);		

		return await Task.FromResult(Results.Ok());
	}
}