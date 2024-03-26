
namespace myhero_dotnet.ContentsAPI;

public static class ContentsExtentions
{
    public static WebApplication ContentsConfigureApplication(this WebApplication app)
    {
        //app.UseSerilogRequestLogging();
        //app.UseHsts();

        //app.UseHttpsRedirection();

        //var textInfo = CultureInfo.CurrentCulture.TextInfo;

        //app.UseSwagger();
        //app.UseSwaggerUI(options =>
        //{
        //    options.RoutePrefix = "swagger";
        //    options.SwaggerEndpoint("v1/swagger.json", $"Shipcret API - {textInfo.ToTitleCase(app.Environment.EnvironmentName)} - V1");
        //});

        app.MapSearchAPI();

        return app;
    }
}