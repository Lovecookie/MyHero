using myhero_dotnet.DatabaseCore.Repositories;


namespace myhero_dotnet.DatabaseCore.DbContexts;

public abstract class DbContextAbstract<TDbContext>
	: DbContext, IUnitOfWork
	where TDbContext : class
{
	protected readonly IMediator? _mediator;
	protected IDbContextTransaction? _transaction;

	public DbContextAbstract(DbContextOptions options) : base(options)
	{
	}

	public DbContextAbstract(DbContextOptions options, IMediator? mediator) : base(options)
	{
	}
	public static string DbName()
	{	
		var attribute = Attribute.GetCustomAttribute(typeof(TDbContext), typeof(DbSchemaAttribute)) as DbSchemaAttribute;
		if (attribute != null)
		{
			return attribute.DbName;
		}

		throw new Exception("Is not supported db name!");
	}

	public static string SchemaName()
	{	
		var attribute = Attribute.GetCustomAttribute(typeof(TDbContext), typeof(DbSchemaAttribute)) as DbSchemaAttribute;
		if (attribute != null)
		{
			return attribute.Schema;
		}

		throw new Exception("Is not supported schema name!");
	}

	public bool HasTransaction => _transaction != null;
	public IDbContextTransaction? Transaction => _transaction;


	public bool HasMediator => _mediator != null;
	public IMediator? Mediator => _mediator;

	public async Task<IDbContextTransaction> BeginTransactionAsync()
	{
		if(HasTransaction)
		{
			return null;
		}

		_transaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

		return _transaction;
	}
}