
namespace Shared.Features.DatabaseCore;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DbSchemaAttribute : System.Attribute
{
	public string ConnectionName { get; init; }

	public string Schema { get; init; }

	public DbSchemaAttribute(string connectionName, string schema)
	{
		ConnectionName = connectionName;
		Schema = schema;
	}
}

//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
//public class DbTableAttribute : System.Attribute
//{ 
//	public string TableName { get; init; }

//	public DbTableAttribute(string tableName)
//	{
//		TableName = tableName;
//	}
//}