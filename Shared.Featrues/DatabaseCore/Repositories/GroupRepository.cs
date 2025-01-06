

namespace Shared.Features.DatabaseCore;


public interface IGroupRepository : IDefaultRepository<ChurchGroup>
{ 

	Task<TOutcome<UserBasic>> Find(Int64 userUid);
	Task<TOutcome<UserBasic>> FindById(string id);
	Task<TOutcome<UserBasic>> FindByEmail(string email);
	Task<TOutcome<UserBasic>> Create(UserBasic entity);

}

public class GroupRepository : IGroupRepository
{
	private readonly AccountDBContext _context;
	private readonly ILogger _logger;
	private readonly TimeProvider _timeProvider;

	public IUnitOfWork UnitOfWork => _context;


	public GroupRepository(AccountDBContext context, ILogger<GroupRepository> logger, TimeProvider timeProvider)
	{ 
		_timeProvider = timeProvider;
		_context = context;
		_logger = logger;
	}

	public async Task<TOutcome<UserBasic>> Find(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<UserBasic>(userUid);
			if(entity == null)
			{
				return TOutcome.Empty<UserBasic>();
			}

			return TOutcome.Success(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<UserBasic>("Not found");
		}
	}

	public async Task<TOutcome<UserBasic>> FindById(string id)
	{
		try
		{
			var entity = await _context.UserBasics
				.Where( e => e.UserID == id)
				.FirstOrDefaultAsync();
			if (entity == null)
			{
				return TOutcome.Empty<UserBasic>();
			}

			return TOutcome.Success(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<UserBasic>(ex.Message);
		}
	}

	public async Task<TOutcome<UserBasic>> FindByEmail(string email)
	{
		try
		{
			var entity = await _context.UserBasics
				.Where(e => e.Email == email)
				.FirstOrDefaultAsync();
			if (entity == null)
			{
				return TOutcome.Empty<UserBasic>();
			}

			return TOutcome.Success(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<UserBasic>(ex.Message);
		}
	}

	public async Task<TOutcome<UserBasic>> Create(UserBasic entity)
	{
		entity.DateCreated = _timeProvider.GetUtcNow().UtcDateTime;
		entity.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

		try
		{
			var newEntry = await _context.AddAsync(entity);
			if( newEntry == null)
			{
				return TOutcome.Unknown<UserBasic>();
			}

			return TOutcome.Success(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<UserBasic>(ex.Message);
		}
	}

}