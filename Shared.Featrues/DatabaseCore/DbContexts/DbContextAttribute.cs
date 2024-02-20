
namespace Shared.Features.DatabaseCore;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DbSchemaAttribute : System.Attribute
{
	public string DbName { get; init; }

	public string Schema { get; init; }

	public DbSchemaAttribute(string name, string schema)
	{
		DbName = name;
		Schema = schema;
	}
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class DbTableAttribute : System.Attribute
{ 
	public string TableName { get; init; }

	public DbTableAttribute(string tableName)
	{
		TableName = tableName;
	}
}