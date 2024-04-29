

namespace Shared.Features.DatabaseCore;

public interface IHowlMessageRepository : IDefaultRepository<HowlMessage>
{ 

	Task<TOptional<HowlMessage>> Find(Int64 userUid);

	Task<TOptional<HowlMessage>> RandomChoice();

	Task<TOptional<List<HowlMessage>>> RandomChoices(Int32 count);

	Task<TOptional<HowlMessage>> Create(HowlMessage entity);	

	// Task<TOptional<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid);

	// Task<TOptional<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID);
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

	public async Task<TOptional<HowlMessage>> Find(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<HowlMessage>(userUid);
			if(entity == null)
			{
				return TOptional.Empty<HowlMessage>();
			}

			return TOptional.Success(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<HowlMessage>("Not found");
		}
	}

	public async Task<TOptional<HowlMessage>> RandomChoice()
	{	
		try
		{
			var query = await _context.HowlMessages
				.OrderBy(e => Guid.NewGuid())
				.FirstOrDefaultAsync();

			if (query == null)
			{
				return TOptional.Empty<HowlMessage>();
			}

			return TOptional.Success(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOptional.Error<HowlMessage>(ex.Message);
		}		
	}	

	public async Task<TOptional<List<HowlMessage>>> RandomChoices(Int32 count)
	{
		try
		{
			var query = await _context.HowlMessages
				.OrderBy(e => Guid.NewGuid())
				.Take(count)
				.ToListAsync();

			if(query.IsNullOrEmpty())
			{
				return TOptional.Empty<List<HowlMessage>>();
			}

			return TOptional.Success(query);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);
			return TOptional.Error<List<HowlMessage>>(ex.Message);
		}
	}

	public async Task<TOptional<HowlMessage>> Create(HowlMessage entity)
	{
		try
		{
			var newEntry = await _context.AddAsync(entity);
			if( newEntry == null)
			{
				return TOptional.Unknown<HowlMessage>();
			}

			return TOptional.Success(newEntry.Entity);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<HowlMessage>(ex.Message);
		}
	}

	// public async Task<TOptional<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid)
	// {
	// 	try
	// 	{
	// 		var query = await (from userBasic in _context.Set<UserBasic>()
	// 					join userPatronage in _context.Set<UserPatronage>()
	// 						on userBasic.UserUID equals userPatronage.UserUID into patronageGroup
	// 					from userPatronageDefault in patronageGroup.DefaultIfEmpty()
	// 					join userRecognition in _context.Set<UserRecognition>()
	// 						on userBasic.UserUID equals userRecognition.UserUID into recognitionGroup
	// 					from userRecognitionDefault in recognitionGroup.DefaultIfEmpty()
	// 					where userBasic.UserUID == userUid
	// 					select new { 
	// 						userBasic,
	// 						userPatronage = userPatronageDefault ?? new UserPatronage(),
	// 						userRecognition = userRecognitionDefault ?? new UserRecognition()
	// 					}).FirstOrDefaultAsync();

	// 		if (query == null)
	// 		{
	// 			return TOptional.Empty<(UserBasic, UserPatronage, UserRecognition)>();
	// 		}

	// 		return TOptional.Success((query.userBasic, query.userPatronage, query.userRecognition));
	// 	}
	// 	catch(Exception ex)
	// 	{
	// 		_logger.LogError(ex.Message);

	// 		return TOptional.Error<(UserBasic, UserPatronage, UserRecognition)>(ex.Message);
	// 	}
	// }

	// public async Task<TOptional<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID)
	// {
	// 	try
	// 	{
	// 		var userBasic = await _context.UserBasics.FindAsync(userUid);
	// 		if (userBasic == null)
	// 		{
	// 			return TOptional.Error<bool>("User not found");
	// 		}

	// 		userBasic.EncryptedUID = encryptedUID;
	// 		//userBasic.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

	// 		return TOptional.Success(true);
	// 	}
	// 	catch (Exception ex)
	// 	{
	// 		_logger.LogError(ex.Message);

	// 		return TOptional.Error<bool>(ex.Message);
	// 	}
	// }	
}