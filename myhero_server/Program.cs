using myhero_dotnet.AccountAPI;
using myhero_dotnet.ContentsAPI;
using myhero_dotnet.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
builder.AddConfigureApplicationBuilder();
builder.AddAccountDependencyInjection();
builder.AddContentsDependencyInjection();
builder.AddUtilInfrastructure();

var app = builder.Build()
	.AccountConfigureApplication()
	.ContentsConfigureApplication()
	.UseInfrastructureMiddlware()
	.UseUtilInfrastructure();

//app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
