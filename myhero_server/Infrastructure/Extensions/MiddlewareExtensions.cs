using myhero_dotnet.Infrastructure.Middleware;

namespace myhero_dotnet.Infrastructure.Extensions;

public static class InfrastructureMiddlewareExtensions
{
    public static WebApplication UseInfrastructureMiddlware(this WebApplication app)
    {
        app.UseMiddleware<FromClientMiddleware>();

        return app;
    }
}