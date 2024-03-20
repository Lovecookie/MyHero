namespace Shared.Features.DatabaseCore;


[PrimaryKey(nameof(UserUID))]
public abstract class UserEntityBase<TEntity> where TEntity : class
{   
    public Int64 UserUID { get; set; } = 0;
	
    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
    
    
 //   public static string TableName()
 //   {
	//	var attr = Attribute.GetCustomAttribute(typeof(TEntity), typeof(DbTableAttribute)) as DbTableAttribute;
	//	if( attr != null )
	//	{
	//		return attr.TableName;
	//	}

	//	throw new Exception("Is not supported table name!");
	//}
}