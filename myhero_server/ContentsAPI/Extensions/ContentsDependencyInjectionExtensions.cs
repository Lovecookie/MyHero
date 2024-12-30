
namespace myhero_dotnet.ContentsAPI;

public static class ContentsDependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddContentsDependencyInjection(this IHostApplicationBuilder builder)
    {
		var services = builder.Services;

		builder.AddNpgSqlDbContext();

		//services.AddAccountCommands();
		//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

		//services.AddHttpContextAccessor();

		//services.AddSingleton(TimeProvider.System);

		//services.AddScoped<IUserBasicRepository, UserBasicRepository>();
		//services.AddScoped<IUserAuthJwtRepository, UserAuthJwtRepository>();

	    services.AddScoped<IHowlMessageRepository, HowlMessageRepository>();
	  	// services.AddScoped<IGroupRepository, GroupRepository>();

		return builder;
    }

	private static void AddNpgSqlDbContext(this IHostApplicationBuilder builder)
	{
        builder.Services.AddDbContextPool<HowlDBContext>(opt =>
            opt
                .UseNpgsql(builder.Configuration.GetConnectionString(HowlDBContext.ConnectionName()))
                .UseSnakeCaseNamingConvention()
        );
	}

	private static void AddContentsCommands(this IServiceCollection services)
    {
		services.AddMediatR(configuration =>
        {
			configuration.RegisterServicesFromAssemblyContaining(typeof(Program));
		});
	}


}