using myhero_dotnet.DatabaseCore.Repositories;


namespace myhero_dotnet.DatabaseCore.DbContexts;

public abstract class DbContextAbstract : DbContext, IUnitOfWork
{
	protected readonly IMediator? _mediator;
	protected IDbContextTransaction? _transaction;

	public DbContextAbstract(DbContextOptions options) : base(options)
	{
	}

	public DbContextAbstract(DbContextOptions options, IMediator? mediator) : base(options)
	{
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