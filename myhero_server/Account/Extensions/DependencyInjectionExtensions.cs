
namespace myhero_dotnet.Account;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddAccountDependencyInjection(this IHostApplicationBuilder builder)
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