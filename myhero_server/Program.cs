
using myhero_dotnet.Account.Extensions;
using myhero_dotnet.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args)
	.AddConfigureApplicationBuilder()
	.AddAccountDependencyInjection();	

builder.Services.AddAuthorization();

var app = builder
	.Build()
	.AccountConfigureApplication()
	.UseInfrastructureMiddlware();

app.Run();
