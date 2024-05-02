using myhero_dotnet.Infrastructure;
using Shared.Featrues.Algorithm;


namespace myhero_dotnet.ContentsAPI;


public static class MessageAPI
{
    public static WebApplication MapMessageAPI(this WebApplication app)
    {
        var apiName = "msg";
        var apiUri = ConstantVersion.URL(apiName);

        var root = app.MapGroup(apiUri)
            .WithGroupName(ConstantVersion.GlobalVersionByLower)
            .WithTags(apiName)
            .WithOpenApi();

        root.MapPost("/sendHowl", SendHowl)
            .WithSummary("Send howl message")
            .WithDescription("\n POST /sendHowl")
            .RequireAuthorization();

        root.MapGet("/getRandomHowl", GetRandomHowl)
            .WithSummary("Get random howl message")
            .WithDescription("\n GET /getRandomHowl")
            .RequireAuthorization();

        Log.Information("[Success MessageAPI mapped");

        return app;
    }

    public static async Task<IResult> SendHowl(
        ClaimsPrincipal principal,
        [FromBody] SendHowlRequest request,
        [AsParameters] ContentsServices services)
    {
        var opt = await services.Mediator.Send(new SendHowlCommand(principal, request.Message));
		if (!opt.HasValue)
        {
			return ToClientResults.Error("Not found.");
		}

		return ToClientResults.Ok(opt.Value!);
    }

    public static async Task<IResult> GetRandomHowl(
        ClaimsPrincipal principal,
        [AsParameters] ContentsServices services )
        {
            var opt = await services.Mediator.Send(new GetRandomHowlCommand(principal));
            if (!opt.HasValue)
            {
                return ToClientResults.Error("Not found.");
            }

            return ToClientResults.Ok(opt.Value!);
        }
}