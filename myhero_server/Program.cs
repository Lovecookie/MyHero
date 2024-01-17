
using myhero_dotnet.Account.Extensions;

var builder = WebApplication.CreateBuilder(args)
	.AddConfigureApplicationBuilder()
	.AddAccountApplicationServices();

builder.Services.AddAuthorization();

var app = builder
	.Build()
	.AccountConfigureApplication();

app.Run();
