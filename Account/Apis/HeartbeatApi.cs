using myhero_dotnet.Account.Requests;
using myhero_dotnet.Account.Services;
using myhero_dotnet.Infrastructure.Commands;
using myhero_dotnet.Infrastructure.Features;


namespace myhero_dotnet.Account.Apis;


public static class HeartbeatApis
{
	public static WebApplication MapHeartbeatApis(this WebApplication app)
	{
		var apiName = "heartbeat";
		var apiUri = $"/api/{ShipcretVersion.GlobalVersionByLower}/{apiName}";

		var root = app.MapGroup($"{apiUri}")
			.WithGroupName(ShipcretVersion.GlobalVersionByLower)
			.WithTags(apiName)
			.WithOpenApi();

		root.MapGet("/", Heartbeat)
			.ProducesProblem(StatusCodes.Status500InternalServerError)
			.WithSummary("Heartbeat")
			.WithDescription("\n GET /heartbeat");

		Serilog.Log.Information("[Success] HeartbeatApis mapped");		
		
		return app;
	}

	public static async Task<IResult> Heartbeat(
		[AsParameters] AccountServices services)
	{
		var heartbeatCommand = new HeartbeatCommand
		{
			HeartBeat = "1"
		};

		var heatbeat = await services.Mediator.Send(heartbeatCommand);

		return Results.Ok(heatbeat!);
	}
}