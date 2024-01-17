

using Microsoft.EntityFrameworkCore.Query;

namespace myhero_dotnet.Infrastructure.Features;


public static class ConstantVersion
{
	public static string ProjectName = "MyHero";
	public static string GlobalVersionByUpper => "V1";
	public static string GlobalVersionByLower => "v1";
}