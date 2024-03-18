using myhero_dotnet.Account;
using myhero_dotnet.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.AddConfigureApplicationBuilder();
builder.AddAccountDependencyInjection();
builder.AddUtilInfrastructure();

var app = builder.Build()
	.AccountConfigureApplication()
	.UseInfrastructureMiddlware()
	.UseUtilInfrastructure();

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
