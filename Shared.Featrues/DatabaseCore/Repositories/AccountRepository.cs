

namespace Shared.Features.DatabaseCore;

public interface IUserBasicRepository : IDefaultRepository<UserBasic>
{ 

	Task<TOutcome<UserBasic>> Find(Int64 userUid);
	Task<TOutcome<UserBasic>> FindById(string id);
	Task<TOutcome<UserBasic>> FindByEmail(string email);
	Task<TOutcome<UserBasic>> Create(UserBasic entity);

	Task<TOutcome<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid);

	Task<TOutcome<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID);
}

public class UserBasicRepository : IUserBasicRepository
{
	private readonly AccountDBContext _context;
	private readonly ILogger _logger;
	private readonly TimeProvider _timeProvider;

	public IUnitOfWork UnitOfWork => _context;


	public UserBasicRepository(AccountDBContext context, ILogger<UserBasicRepository> logger, TimeProvider timeProvider)
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

	public async Task<TOutcome<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid)
	{
		try
		{
			var query = await (from userBasic in _context.Set<UserBasic>()
						join userPatronage in _context.Set<UserPatronage>()
							on userBasic.UserUID equals userPatronage.UserUID into patronageGroup
						from userPatronageDefault in patronageGroup.DefaultIfEmpty()
						join userRecognition in _context.Set<UserRecognition>()
							on userBasic.UserUID equals userRecognition.UserUID into recognitionGroup
						from userRecognitionDefault in recognitionGroup.DefaultIfEmpty()
						where userBasic.UserUID == userUid
						select new { 
							userBasic,
							userPatronage = userPatronageDefault ?? new UserPatronage(),
							userRecognition = userRecognitionDefault ?? new UserRecognition()
						}).FirstOrDefaultAsync();

			if (query == null)
			{
				return TOutcome.Empty<(UserBasic, UserPatronage, UserRecognition)>();
			}

			return TOutcome.Success((query.userBasic, query.userPatronage, query.userRecognition));
		}
		catch(Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<(UserBasic, UserPatronage, UserRecognition)>(ex.Message);
		}
	}

	public async Task<TOutcome<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID)
	{
		try
		{
			var userBasic = await _context.UserBasics.FindAsync(userUid);
			if (userBasic == null)
			{
				return TOutcome.Error<bool>("User not found");
			}

			userBasic.EncryptedUID = encryptedUID;
			//userBasic.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

			return TOutcome.Success(true);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Error<bool>(ex.Message);
		}
	}	
}