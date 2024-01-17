using myhero_dotnet.Account.Services;
using myhero_dotnet.DatabaseCore.DbContexts;
using myhero_dotnet.DatabaseCore.Repositories;

namespace myhero_dotnet.Account.Extensions;

public static class AccountExtensions
{
    public static WebApplicationBuilder AddAccountApplicationServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
		});

        builder.AddNpgsqlDbContext<AccountDbContext>("accountdb",
            settings => settings.DbContextPooling = false,
            configureDbContextOptions: builder =>
            {
                builder.UseSnakeCaseNamingConvention();
            });

		services.AddHttpContextAccessor();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IUserBasicRepository, UserBasicRepository>();

		return builder;
    }

}