

namespace shipcret_server_dotnet.Account.DependencyInjection;

public static class AccountDependencyInjection
{
	public static IServiceCollection AddAccountInfrastructure(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());		

		return services;
	}

}