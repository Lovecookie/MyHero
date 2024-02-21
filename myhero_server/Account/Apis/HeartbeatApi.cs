using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.Account;


public static class HeartbeatApi
{
	public static WebApplication MapHeartbeatApis(this WebApplication app)
	{
		var apiName = "heartbeat";
		var apiUri = ConstantVersion.URL(apiName);

		var root = app.MapGroup($"{apiUri}")
			.WithGroupName(ConstantVersion.GlobalVersionByLower)
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