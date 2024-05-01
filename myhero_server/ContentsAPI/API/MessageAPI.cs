﻿using myhero_dotnet.Infrastructure;
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

        root.MapPost("/howl", SendHowl)
            .WithSummary("Howl Message")
            .WithDescription("\n POST /howl");

        Log.Information("[Success MessageAPI mapped");

        return app;
    }

    public static async Task<IResult> SendHowl(
        [FromBody] SendHowlRequest request,
        [AsParameters] ContentsServices services,
        string uid)
    {
        var decryptedUID = await AesWrapper.DecryptAsInt64(uid);
        if(!decryptedUID.HasValue)
        {
			return ToClientResults.Error("Invalid ID.");
		}

        var opt = await services.Mediator.Send(new SendHowlCommand(decryptedUID.Value));
		if (!opt.HasValue)
        {
			return ToClientResults.Error("Not found.");
		}

		return ToClientResults.Ok(opt.Value!);
    }
}