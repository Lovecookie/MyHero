

namespace Shared.Features.DatabaseCore;

public interface IHowlMessageRepository : IDefaultRepository<UserHowl>
{ 

	Task<TOptional<UserHowl>> Find(Int64 userUID);

	Task<TOptional<UserHowl>> GetRandom(Int64 userUID);

	Task<TOptional<List<UserHowl>>> GetRandoms(Int64 userUID, Int32 count);

	Task<TOptional<UserHowl>> Create(UserHowl entity);
}

public class HowlMessageRepository : IHowlMessageRepository
{
	private readonly HowlDBContext _context;
	private readonly ILogger _logger;
	private readonly TimeProvider _timeProvider;

	public IUnitOfWork UnitOfWork => _context;


	public HowlMessageRepository(HowlDBContext context, ILogger<HowlMessageRepository> logger, TimeProvider timeProvider)
	{ 
		_timeProvider = timeProvider;
		_context = context;
		_logger = logger;
	}

	public async Task<TOptional<UserHowl>> Find(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<UserHowl>(userUid);
			if(entity == null)
			{
				return TOptional.Empty<UserHowl>();
			}

			return TOptional.Success(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserHowl>("Not found");
		}
	}

	public async Task<TOptional<UserHowl>> GetRandom(Int64 userUID)
	{	
		try
		{
			var query = await _context.UserHowls
				.Where(e => e.UserUID == userUID)
				.OrderBy(e => Guid.NewGuid())
				.FirstOrDefaultAsync();

			if (query == null)
			{
				return TOptional.Empty<UserHowl>();
			}

			return TOptional.Success(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOptional.Error<UserHowl>(ex.Message);
		}		
	}	

	public async Task<TOptional<List<UserHowl>>> GetRandoms(Int64 userUID, Int32 count)
	{
		try
		{
			var query = await _context.UserHowls
				.Where(e => e.UserUID == userUID)
				.OrderBy(e => Guid.NewGuid())
				.Take(count)
				.ToListAsync();
			
			if(query == null)
			{
				return TOptional.Empty<List<UserHowl>>();
			}

			return TOptional.Success(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOptional.Error<List<UserHowl>>(ex.Message);
		}
	}

	public async Task<TOptional<UserHowl>> Create(UserHowl entity)
	{
		try
		{
			var newEntry = await _context.AddAsync(entity);
			if( newEntry == null)
			{
				return TOptional.Unknown<UserHowl>();
			}

			return TOptional.Success(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<UserHowl>(ex.Message);
		}
	}
}