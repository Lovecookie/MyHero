
namespace myhero_dotnet.Account;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddAccountDependencyInjection(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

		builder.AddNpgSqlDbContext();

		services.AddAccountCommands();

		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		services.AddHttpContextAccessor();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IUserBasicRepository, UserBasicRepository>();
        services.AddScoped<IUserAuthJwtRepository, UserAuthJwtRepository>();

		return builder;
    }	

    private static void AddAccountCommands(this IServiceCollection services)
    {
		services.AddMediatR(configuration =>
        {
			configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
		});
	}

    private static void AddNpgSqlDbContext(this IHostApplicationBuilder builder)
    {
		builder.AddNpgsqlDbContext<AccountDbContext>(AccountDbContext.ConnectionName(),
			settings => settings.DbContextPooling = false,
			configureDbContextOptions: builder =>
			{
				builder.UseSnakeCaseNamingConvention();
			});

		builder.AddNpgsqlDbContext<AuthDbContext>(AuthDbContext.ConnectionName(),
			settings => settings.DbContextPooling = false,
			configureDbContextOptions: builder =>
			{
				builder.UseSnakeCaseNamingConvention();
			});
	}
}