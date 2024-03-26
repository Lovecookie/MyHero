

using myhero_dotnet.ContentsAPI;

namespace myhero_dotnet.AccountAPI;

public static class AccountExtentions
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

		app.MapAuthAPI();
		app.MapAccountAPI();
        app.MapHeartbeatAPI();

        app.MapSearchAPI();

        return app;
    }
}