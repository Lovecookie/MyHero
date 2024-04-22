

namespace Shared.Features.DatabaseCore;

public interface IGroupRepository : IDefaultRepository<ChurchGroup>
{ 

	Task<TOptional<UserBasic>> Find(Int64 userUid);
	Task<TOptional<UserBasic>> FindById(string id);
	Task<TOptional<UserBasic>> FindByEmail(string email);
	Task<TOptional<UserBasic>> Create(UserBasic entity);

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

	public async Task<TOptional<UserBasic>> Find(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<UserBasic>(userUid);
			if(entity == null)
			{
				return TOptional.Empty<UserBasic>();
			}

			return TOptional.Success(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserBasic>("Not found");
		}
	}

	public async Task<TOptional<UserBasic>> FindById(string id)
	{
		try
		{
			var entity = await _context.UserBasics
				.Where( e => e.UserID == id)
				.FirstOrDefaultAsync();
			if (entity == null)
			{
				return TOptional.Empty<UserBasic>();
			}

			return TOptional.Success(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserBasic>(ex.Message);
		}
	}

	public async Task<TOptional<UserBasic>> FindByEmail(string email)
	{
		try
		{
			var entity = await _context.UserBasics
				.Where(e => e.Email == email)
				.FirstOrDefaultAsync();
			if (entity == null)
			{
				return TOptional.Empty<UserBasic>();
			}

			return TOptional.Success(entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserBasic>(ex.Message);
		}
	}

	public async Task<TOptional<UserBasic>> Create(UserBasic entity)
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

			return TOptional.Success(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserBasic>(ex.Message);
		}
	}

}