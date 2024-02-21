

namespace myhero_dotnet.Infrastructure;


public static class ConstantVersion
{
	public static string ProjectName = "MyHero";
	public static string GlobalVersionByUpper => "V1";
	public static string GlobalVersionByLower => "v1";

	public static string URL(string name) => $"/api/{GlobalVersionByLower}/{name}";	
}