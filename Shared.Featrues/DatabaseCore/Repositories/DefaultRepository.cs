namespace Shared.Features.DatabaseCore;
public interface IAggregateRoot { }

public interface IUnitOfWork : IDisposable
{   
}


public interface IDefaultRepository<TEntity> where TEntity : IAggregateRoot
{
	IUnitOfWork UnitOfWork { get; }
}
