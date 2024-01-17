

using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;
using myhero_dotnet.Account.DependencyInjection;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace myhero_dotnet.Extenstions;

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
				Title = $"Shipcret API - {textInfo.ToTitleCase(builder.Environment.EnvironmentName)}",
				Description = "Shipcret API .NET 8",
				Contact = new OpenApiContact
				{
					Name = "Shipcret API",
					Email = "silver2000cs@gmail.com",
					Url = new Uri("https://github.com/Lovecookie/mystyler"),
				},
				License = new OpenApiLicense
				{
					Name = "Shipcret API - License - MIT",
					Url = new Uri("https://opensource.org/licenses/MIT")
				},
				TermsOfService = new Uri("https://github.com/Lovecookie/mystyler")
			});

			var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

			//options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
			//options.DocInclusionPredicate((name, api) => true);

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		});


		services.AddAccountInfrastructure();
		services.AddApplication();		

		return builder;
	}
}