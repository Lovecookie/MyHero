using MediatR;
using Microsoft.Extensions.ServiceDiscovery.Abstractions;
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
			.WithSummary("SignUp")
			.WithDescription("\n POST /signup");

		root.MapPost("/login", Login)
			.WithSummary("Login User")
			.WithDescription("\n POST /login");		

		root.MapPost("/refresh", Refresh)
			.WithSummary("Refresh Token")
			.WithDescription("\n POST /refresh");

		root.MapPost("/logout", Logout )
			.WithSummary("Logout User")
			.WithDescription("\n POST /logout")
			.RequireAuthorization();

		root.MapGet("/user", GetUser)
			.WithSummary("Get User")
			.WithDescription("\n GET /user")
			.RequireAuthorization();

		Serilog.Log.Information("[Success] AuthApis mapped");

		return app;
	}

	public static async Task<IResult> SignUp(
		[FromBody] CreateUserRequest createUserRequest,
		[AsParameters] AccountServices services
		)
	{
		var createUserCommand = services.Mapper.Map<CreateUserCommand>(createUserRequest);

		var createUserOpt = await services.Mediator.Send(createUserCommand);
		if(!createUserOpt.HasValue)
		{ 
			return ToClientResults.Error("create user failed.");
		}

		var jwtCommand = new JwtHmacSha256Command(new SharedLoginRequest { Email = createUserRequest.Email!, Password = createUserRequest.Pw! });

		var token = await services.Mediator.Send(jwtCommand);

		return ToClientResults.Ok();
	}


	public static async Task<IResult> Login(
		[FromBody] LoginUserRequest loginUserRequest,
		[AsParameters] AccountServices services)
	{
		var jwtCommand = new JwtHmacSha256Command(new SharedLoginRequest()
		{
			Email = loginUserRequest.Email!,
			Password = loginUserRequest.Pw!
		});	

		var opt = await services.Mediator.Send(jwtCommand);
        if (!opt.HasValue)
		{
			return ToClientResults.Error("authencation jwt invalid.");
		}        

        return Results.Ok($"token:{opt.Value}");
	}

	public static async Task<IResult> Refresh(
		[FromBody] RefreshTokenRequest refreshTokenRequest,
		[AsParameters] AccountServices services)
	{
		return await Task.FromResult(Results.Ok());
	}

	public static async Task<IResult> Logout(ClaimsPrincipal user)
	{
		return await Task.FromResult(Results.Ok());
	}


	public static async Task<IResult> GetUser(ClaimsPrincipal user)
	{
		await Task.CompletedTask.WaitAsync(TimeSpan.Zero);

		var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        if (emailClaim == null)
		{	
			return Results.NotFound("Unauthorized.");
		}

		return Results.Ok($"email:{emailClaim.Value}");
	}
}