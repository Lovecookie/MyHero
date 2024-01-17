
using myhero_dotnet.Account.Extensions;
using myhero_dotnet.Account.Extenstions;
using myhero_dotnet.Extenstions;

var builder = WebApplication.CreateBuilder(args)
	.AddConfigureApplicationBuilder()
	.AddAccountApplicationServices();

builder.Services.AddAuthorization();

var app = builder
	.Build()
	.AccountConfigureApplication();

app.Run();
