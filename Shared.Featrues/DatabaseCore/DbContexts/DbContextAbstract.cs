
namespace Shared.Features.DatabaseCore;

public abstract class DBContextAbstract<TDBContext>
	: DbContext, IUnitOfWork
	where TDBContext : class
{
	protected readonly IMediator? _mediator;
	protected readonly ILogger _logger;
	protected IDbContextTransaction? _transaction;
	protected IConfiguration _configuration;

	public DBContextAbstract(DbContextOptions options, ILogger<DBContextAbstract<TDBContext>> logger, IConfiguration configuration ) : base(options)
	{
		_logger = logger;
		_configuration = configuration;
	}

	public DBContextAbstract(DbContextOptions options, ILogger<DBContextAbstract<TDBContext>> logger, IMediator? mediator, IConfiguration configuration ) : base(options)
	{
		_logger = logger;
		_mediator = mediator;
		_configuration = configuration;
	}

	public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
	{
		//if (_mediator != null)
		//{
		//	await _mediator.DispatchDomainEventAsync(this);
		//}

		try
		{ 	
			await base.SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateException ex)
		{
			_logger.LogError(ex, ex.Message);
		}		

		return true;
	}

	public static string ConnectionName()
	{	
		var attribute = Attribute.GetCustomAttribute(typeof(TDBContext), typeof(SharedDbSchemaAttribute)) as SharedDbSchemaAttribute;
		if (attribute != null)
		{
			return attribute.ConnectionName;
		}

		throw new Exception("Is not supported db name!");
	}

	//public static string DatabaseName()
	//{
	//	var attribute = Attribute.GetCustomAttribute(typeof(TDBContext), typeof(SharedDbSchemaAttribute)) as SharedDbSchemaAttribute;
	//	if (attribute != null)
	//	{
	//		return attribute.DatabaseName;
	//	}

	//	throw new Exception("Is not supported database name!");
	//}

	public static string SchemaName()
	{	
		var attribute = Attribute.GetCustomAttribute(typeof(TDBContext), typeof(SharedDbSchemaAttribute)) as SharedDbSchemaAttribute;
		if (attribute != null)
		{
			return attribute.Schema;
		}

		throw new Exception("Is not supported schema name!");
	}

	public ILogger Logger => _logger;

	public bool HasTransaction => _transaction != null;
	public IDbContextTransaction? Transaction => _transaction;


	public bool HasMediator => _mediator != null;
	public IMediator? Mediator => _mediator;

	public async Task<IDbContextTransaction?> BeginTransactionAsync()
	{
		if(HasTransaction)
		{
			return null;
		}

		_transaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

		return _transaction;
	}


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		#region ms-sql
		// ms-sql
		// optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
		#endregion

		#region postgres
		// postgres
		optionsBuilder.UseNpgsql(
						_configuration.GetConnectionString(ConnectionName()));
		#endregion
	}
}