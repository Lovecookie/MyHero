using myhero_dotnet.Infrastructure.Middleware;

namespace myhero_dotnet.Infrastructure;

public static class InfrastructureMiddlewareExtensions
{
    public static WebApplication UseInfrastructureMiddlware(this WebApplication app)
    {
        app.UseMiddleware<FromClientMiddleware>();

        return app;
    }
}