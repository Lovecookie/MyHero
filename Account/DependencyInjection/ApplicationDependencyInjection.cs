

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace shipcret_server_dotnet.Account.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

}