
namespace myhero_dotnet.ContentsAPI;

public static class ContentsDependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddContentsDependencyInjection(this IHostApplicationBuilder builder)
    {
		var services = builder.Services;

		//builder.AddNpgSqlDbContext();

		//services.AddAccountCommands();

		//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		//services.AddHttpContextAccessor();

  //      services.AddSingleton(TimeProvider.System);

  //      services.AddScoped<IUserBasicRepository, UserBasicRepository>();
  //      services.AddScoped<IUserAuthJwtRepository, UserAuthJwtRepository>();

		return builder;
    }

	private static void AddNpgSqlDbContext(this IHostApplicationBuilder builder)
	{
		//builder.AddNpgsqlDbContext<AccountDBContext>(AccountDBContext.ConnectionName(),
		//	settings => settings.DbContextPooling = false,
		//	configureDbContextOptions: builder =>
		//	{
		//		builder.UseSnakeCaseNamingConvention();
		//	});

		//builder.AddNpgsqlDbContext<AuthDBContext>(AuthDBContext.ConnectionName(),
		//	settings => settings.DbContextPooling = false,
		//	configureDbContextOptions: builder =>
		//	{
		//		builder.UseSnakeCaseNamingConvention();
		//	});
	}

	private static void AddContentsCommands(this IServiceCollection services)
    {
		services.AddMediatR(configuration =>
        {
			configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
		});
	}


}