using shipcret_server_dotnet.Account.Services;
using shipcret_server_dotnet.DatabaseCore.DbContexts;
using shipcret_server_dotnet.DatabaseCore.Repositories;

namespace shipcret_server_dotnet.Account.Extensions;

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