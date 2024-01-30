using myhero_dotnet.Account.Services;
using myhero_dotnet.DatabaseCore.DbContexts;
using myhero_dotnet.DatabaseCore.Repositories;

namespace myhero_dotnet.Account.Extensions;

public static class DependencyInjectionExtensions
{
    public static WebApplicationBuilder AddAccountDependencyInjection(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
		});

        builder.AddNpgsqlDbContext<AccountDbContext>(AccountDbContext.SchemaName(),
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