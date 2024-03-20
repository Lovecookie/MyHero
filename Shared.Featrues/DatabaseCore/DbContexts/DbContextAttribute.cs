
namespace Shared.Features.DatabaseCore;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DbSchemaAttribute : Attribute
{
	public string ConnectionName { get; init; }

	public string Schema { get; init; }

	public DbSchemaAttribute(string connectionName, string schema)
	{
		ConnectionName = connectionName;
		Schema = schema;
	}
}
