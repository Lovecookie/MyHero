
using myhero_dotnet.Account.Extensions;
using myhero_dotnet.Infrastructure.Extensions;
using Util.Infrastructure.DependencyInjection;
using Util.Infrastructure.Extensions;

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
