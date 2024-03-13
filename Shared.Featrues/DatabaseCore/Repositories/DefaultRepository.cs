namespace Shared.Features.DatabaseCore;
public interface IAggregateRoot { }

public interface IUnitOfWork : IDisposable
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}


public interface IDefaultRepository<TEntity> where TEntity : IAggregateRoot
{
	IUnitOfWork UnitOfWork { get; }	
}
