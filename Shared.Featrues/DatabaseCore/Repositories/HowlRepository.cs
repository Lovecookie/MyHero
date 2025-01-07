

namespace Shared.Features.DatabaseCore;

public interface IHowlMessageRepository : IDefaultRepository<UserHowl>
{ 

	Task<TOutcome<UserHowl>> Find(Int64 userUID);

	Task<TOutcome<UserHowl>> GetRandom(Int64 userUID);

	Task<TOutcome<List<UserHowl>>> GetRandoms(Int64 userUID, Int32 count);

	Task<TOutcome<UserHowl>> Create(UserHowl entity);
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

	public async Task<TOutcome<UserHowl>> Find(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<UserHowl>(userUid);
			if(entity == null)
			{
				return TOutcome.Empty<UserHowl>();
			}

			return TOutcome.Ok(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Err<UserHowl>("Not found");
		}
	}

	public async Task<TOutcome<UserHowl>> GetRandom(Int64 userUID)
	{	
		try
		{
			var query = await _context.UserHowls
				.Where(e => e.UserUID == userUID)
				.OrderBy(e => Guid.NewGuid())
				.FirstOrDefaultAsync();

			if (query == null)
			{
				return TOutcome.Empty<UserHowl>();
			}

			return TOutcome.Ok(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOutcome.Err<UserHowl>(ex.Message);
		}		
	}	

	public async Task<TOutcome<List<UserHowl>>> GetRandoms(Int64 userUID, Int32 count)
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
				return TOutcome.Empty<List<UserHowl>>();
			}

			return TOutcome.Ok(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOutcome.Err<List<UserHowl>>(ex.Message);
		}
	}

	public async Task<TOutcome<UserHowl>> Create(UserHowl entity)
	{
		try
		{
			var newEntry = await _context.AddAsync(entity);
			if( newEntry == null)
			{
				return TOutcome.Unknown<UserHowl>();
			}

			return TOutcome.Ok(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Err<UserHowl>(ex.Message);
		}
	}
}