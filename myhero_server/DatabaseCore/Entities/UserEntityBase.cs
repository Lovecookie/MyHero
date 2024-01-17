using Microsoft.EntityFrameworkCore;
using myhero_dotnet.DatabaseCore.DbContexts;

namespace myhero_dotnet.DatabaseCore.Entities;

public abstract class UserEntityBase<TEntity> where TEntity : class
{
    [Key]
    public long UserUid { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
    
    
    public static string TableName()
    {
		var attr = Attribute.GetCustomAttribute(typeof(TEntity), typeof(DbTableAttribute)) as DbTableAttribute;
		if( attr != null )
		{
			return attr.TableName;
		}

		throw new Exception("Is not supported table name!");
	}
}