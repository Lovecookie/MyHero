


using Serilog;

using myhero_dotnet.Infrastructure.Features;

namespace myhero_dotnet.Account.Extensions;

public static class DefaultBuilderExtensions
{
    public static WebApplicationBuilder AddConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.Host.UseSerilog((hostContext, loggerConfiguration) =>
        {
            var assembly = Assembly.GetEntryAssembly();

            loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration)
            .Enrich.WithProperty("Assembly Version", assembly?.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version)
            .Enrich.WithProperty("Aassembly Informational Version", assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion);

            loggerConfiguration.WriteTo.Console();
        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        var textInfo = CultureInfo.CurrentCulture.TextInfo;

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = $"{ConstantVersion.ProjectName} API - {textInfo.ToTitleCase(builder.Environment.EnvironmentName)}",
                Description = $"{ConstantVersion.ProjectName} API .NET 8",
                Contact = new OpenApiContact
                {
                    Name = $"{ConstantVersion.ProjectName} API",
                    Email = "block9002@gmail.com, silver2000cs@gmail.com",
                    Url = new Uri("https://github.com/Lovecookie/myhero"),
                },
                License = new OpenApiLicense
                {
                    Name = $"{ConstantVersion.ProjectName} API - Unlicense",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                TermsOfService = new Uri("https://github.com/Lovecookie/myhero")
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            //options.DocInclusionPredicate((name, api) => true);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });

        return builder;
    }
}