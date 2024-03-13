
using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.Account;

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
			.WithSummary("Logout user")
			.WithDescription("\n POST /logout")
			.RequireAuthorization();

		root.MapGet("/user", GetUser)
			.WithSummary("Get user")
			.WithDescription("\n GET /user")
			.RequireAuthorization();

		Serilog.Log.Information("[Success] AuthApis mapped");

		return app;
	}

	public static async Task<IResult> SignUp(
		[FromBody] CreateUserRequest createUserRequest,
		[AsParameters] AuthServices services
		)
	{
		var createUserCommand = services.Mapper.Map<CreateUserCommand>(createUserRequest);

		var createUserOpt = await services.Mediator.Send(createUserCommand);
		if (!createUserOpt.HasValue)
		{
			return ToClientResults.Error(createUserOpt.Message);
		}

		return ToClientResults.Ok(new CreateUserResponse(createUserOpt.Value!.Item2));
	}

	public static async Task<IResult> SignIn(
		[FromBody] LoginUserRequest loginUserRequest,
		[AsParameters] AuthServices services)
	{
		var tupleOpt = await services.Mediator.Send(new SignInCommand(loginUserRequest.Email!, loginUserRequest.Pw!));
		if(!tupleOpt.HasValue)
		{
			return ToClientResults.Error(tupleOpt.Message);
		}

		return ToClientResults.Ok(new SignInResponse(tupleOpt.Value!.Item2));
	}

	public static async Task<IResult> Refresh(
		//[FromBody] RefreshTokenRequest refreshTokenRequest,
		ClaimsPrincipal principal,
		[AsParameters] AuthServices services)
	{
		var tokenType = principal.FindFirstValue(CustomClaimType.TokenType);
		if(tokenType == null || tokenType != "refresh")
		{
			return ToClientResults.Error("Unauthorized");
		}

		var userUIDClaim = principal.FindFirstValue(CustomClaimType.UserUID);
		if(userUIDClaim == null)
		{
			return ToClientResults.Error("Unauthorized");
		}

		var refreshJwtOpt = await services.Mediator.Send(new RefreshJwtCommand(Convert.ToInt64(userUIDClaim)));
		if(!refreshJwtOpt.HasValue)
		{
			return ToClientResults.Error(refreshJwtOpt.Message);
		}

		return ToClientResults.Ok(new RefreshTokenResposne(refreshJwtOpt.Value!));
	}

	public static async Task<IResult> Logout(ClaimsPrincipal user)
	{
		return await Task.FromResult(Results.Ok());
	}


	public static async Task<IResult> GetUser(ClaimsPrincipal principal)
	{
		await Task.CompletedTask.WaitAsync(TimeSpan.Zero);

		var userUIDClaim = principal.FindFirstValue(CustomClaimType.UserUID);
		if(userUIDClaim == null)
		{
			return ToClientResults.Error("Unauthorized");
		}

		return Results.Ok();
	}
}