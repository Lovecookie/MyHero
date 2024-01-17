
using myhero_dotnet.DatabaseCore.DbContexts;
using myhero_dotnet.DatabaseCore.Entities;


namespace myhero_dotnet.DatabaseCore.Repositories;

public interface IUserBasicRepository : IDefaultRepository<UserBasic>
{
	Task<UserBasic> FindAsync(Int64 UserUid);
	Task<UserBasic> CreateAsync(UserBasic entity, CancellationToken cancellationToken);
	
}

internal class UserBasicRepository : IUserBasicRepository
{
	private readonly AccountDbContext _context;
	private readonly ILogger _logger;
	private readonly TimeProvider _timeProvider;

	public IUnitOfWork UnitOfWork => _context;
	

	public UserBasicRepository(AccountDbContext context, ILogger<UserBasicRepository> logger, TimeProvider timeProvider)
	{ 
		_timeProvider = timeProvider;
		_context = context;
		_logger = logger;
	}

	public async Task<UserBasic> FindAsync(Int64 UserUid)
	{
		try
		{
			var findedEntity = _context.Find<UserBasic>(UserUid);
			if (findedEntity == null)
			{

			}
		}
		catch (Exception ex)
		{

		}
	}

	public async Task<UserBasic> CreateAsync(UserBasic entity, CancellationToken cancellationToken)
	{
		entity.DateCreated = _timeProvider.GetUtcNow().UtcDateTime;
		entity.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

		try
		{
			var newEntity = _context.Add(entity).Entity;

			var result = await _context.SaveChangesAsync(cancellationToken);

			return newEntity;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return new();
		}
	}
}