


using Serilog;
using shipcret_server_dotnet.Account.Apis;
using System.Globalization;

namespace shipcret_server_dotnet.Account.Extenstions;

public static class DefaultExtentions
{
	public static WebApplication AccountConfigureApplication(this WebApplication app)
	{
		app.UseSerilogRequestLogging();

		app.UseHsts();

		//app.UseHttpsRedirection();

		var textInfo = CultureInfo.CurrentCulture.TextInfo;

		app.UseSwagger();
		app.UseSwaggerUI(options =>
		{
			options.RoutePrefix = "swagger";
			options.SwaggerEndpoint("v1/swagger.json", $"Shipcret API - {textInfo.ToTitleCase(app.Environment.EnvironmentName)} - V1");
		});

		app.MapAccountApis();
		app.MapHeartbeatApis();

		return app;
	}
}