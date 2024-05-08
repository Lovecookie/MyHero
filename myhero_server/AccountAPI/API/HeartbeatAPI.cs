using myhero_dotnet.Infrastructure;

namespace myhero_dotnet.AccountAPI;


public static class HeartbeatAPI
{
	public static WebApplication MapHeartbeatAPI(this WebApplication app)
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

		Serilog.Log.Information("[Success] MapHeartbeatAPI mapped");		
		
		return app;
	}

	public static async Task<IResult> Heartbeat(
		[AsParameters] AccountServices services)
	{
		var heatbeat = await services.Mediator.Send(new HeartbeatCommand(Heartbeat: "1"));

		return Results.Ok(heatbeat!);
	}
}