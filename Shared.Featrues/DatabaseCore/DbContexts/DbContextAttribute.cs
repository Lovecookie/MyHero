
namespace Shared.Features.DatabaseCore;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class SharedDbSchemaAttribute : Attribute
{
	public string ConnectionName { get; init; }	

	public string Schema { get; init; }

	public SharedDbSchemaAttribute(string connectionName, string schema)
	{
		ConnectionName = connectionName;
		Schema = schema;
	}
}
