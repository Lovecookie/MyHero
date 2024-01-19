
using Microsoft.AspNetCore.SignalR;
using myhero_dotnet.CommonFeatures.GenericObjects;
using myhero_dotnet.DatabaseCore.DbContexts;
using myhero_dotnet.DatabaseCore.Entities;
using System.Threading;


namespace myhero_dotnet.DatabaseCore.Repositories;

public interface IUserBasicRepository : IDefaultRepository<UserBasic>
{
	Task<TOptional<UserBasic>> FindAsync(Int64 userUid);
	Task<TOptional<UserBasic>> FindByIdAsync(string id);
	Task<TOptional<UserBasic>> CreateAsync(UserBasic entity, CancellationToken cancellationToken);	
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

	public async Task<TOptional<UserBasic>> FindAsync(Int64 userUid)
	{
		try
		{
			var findedEntity = _context.Find<UserBasic>(new { UserUid = userUid });
			if (findedEntity == null)
			{

			}
		}
		catch (Exception ex)
		{

		}
	}

	public async Task<TOptional<UserBasic>> FindByIdAsync(string id)
	{
		try
		{
			var findedEntity = await _context.FindAsync<UserBasic>(new { UserId = id });
			if (findedEntity == null)
			{
				return TOptional.Empty<UserBasic>();
			}

			return TOptional.To(findedEntity);
		}
		catch (Exception ex)
		{
			return TOptional.Error<UserBasic>(ex.Message);
		}
	}

	public async Task<TOptional<UserBasic>> CreateAsync(UserBasic entity, CancellationToken cancellationToken)
	{
		entity.DateCreated = _timeProvider.GetUtcNow().UtcDateTime;
		entity.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

		try
		{
			var newEntry = await _context.AddAsync(entity);
			if( newEntry == null)
			{
				return TOptional.Unknown<UserBasic>();
			}

			var result = await _context.SaveChangesAsync(cancellationToken);

			return TOptional.To(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserBasic>(ex.Message);
		}
	}
}