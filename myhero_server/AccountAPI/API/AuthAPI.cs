
using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.AccountAPI;

public static class AuthAPI
{
	public static WebApplication MapAuthAPI(this WebApplication app)
	{
		var apiName = "auth";
		var apiUri = ConstantVersion.URL(apiName);

		var root = app.MapGroup(apiUri)
			.WithGroupName(ConstantVersion.GlobalVersionByLower)
			.WithTags(apiName)
			.WithOpenApi();

		root.MapPost("/signup", SignUp)
			.WithSummary("Sign up")
			.WithDescription("\n POST /signup");

		root.MapPost("/signin", SignIn)
			.WithSummary("Sign in user")
			.WithDescription("\n POST /login");

		root.MapPost("/refresh", Refresh)
			.WithSummary("Refresh token")
			.WithDescription("\n POST /refresh");

		root.MapPost("/logout", Logout)
			.WithSummary("Logout")
			.WithDescription("\n POST /logout")
			.RequireAuthorization();

		Serilog.Log.Information("[Success] MapAuthAPI mapped");

		return app;
	}

	public static async Task<IResult> SignUp(
		[FromBody] CreateUserRequest createUserRequest,
		[AsParameters] AuthServices services
		)
	{
		var createUserOpt = await services.Mediator.Send(new CreateUserCommand(createUserRequest.UserId!,
			createUserRequest.Email!,
			createUserRequest.Pw!,
			""));
		if (!createUserOpt.HasValue)
		{
			return ToClientResults.Error(createUserOpt.Message);
		}

		return ToClientResults.Ok(new CreateUserResponse(createUserOpt.Value!));
	}

	public static async Task<IResult> SignIn(
		[FromBody] LoginUserRequest loginUserRequest,
		[AsParameters] AuthServices services)
	{
		var commandOpt = await services.Mediator.Send(new SignInCommand(loginUserRequest.Email!, loginUserRequest.Pw!));
		if(!commandOpt.HasValue)
		{
			return ToClientResults.Error(commandOpt.Message);
		}

		return ToClientResults.Ok(commandOpt.Value!);
	}

	public static async Task<IResult> Refresh(		
		ClaimsPrincipal principal,
		[AsParameters] AuthServices services)
	{
		var refreshJwtOpt = await services.Mediator.Send(new RefreshJwtCommand(principal));
		if(!refreshJwtOpt.HasValue)
		{
			return ToClientResults.Error(refreshJwtOpt.Message);
		}

		return ToClientResults.Ok(new RefreshTokenResposne(refreshJwtOpt.Value!));
	}

	public static async Task<IResult> Logout(ClaimsPrincipal user,
		[AsParameters] AuthServices services)
	{
		var logoutOpt = await services.Mediator.Send(new LogoutUserCommand(user));
		if(!logoutOpt.HasValue)
		{
			return ToClientResults.Error(logoutOpt.Message);
		}

		return ToClientResults.Ok();
	}

}