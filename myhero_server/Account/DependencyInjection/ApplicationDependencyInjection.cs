

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace myhero_dotnet.Account.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

}