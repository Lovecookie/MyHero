
using shipcret_server_dotnet.Account.Extensions;
using shipcret_server_dotnet.Account.Extenstions;
using shipcret_server_dotnet.Extenstions;

var builder = WebApplication.CreateBuilder(args)
	.AddConfigureApplicationBuilder()
	.AddAccountApplicationServices();

builder.Services.AddAuthorization();

var app = builder
	.Build()
	.AccountConfigureApplication();

app.Run();
