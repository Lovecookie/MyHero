

namespace Shared.Features.DatabaseCore;

public interface IUserBasicRepository : IDefaultRepository<UserBasic>
{ 

	Task<TOptional<UserBasic>> Find(Int64 userUid);
	Task<TOptional<UserBasic>> FindById(string id);
	Task<TOptional<UserBasic>> FindByEmail(string email);
	Task<TOptional<UserBasic>> Create(UserBasic entity);

	Task<TOptional<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid);

	Task<TOptional<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID);
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

	public async Task<TOptional<(UserBasic, UserPatronage, UserRecognition)>> SelectUserBasic(Int64 userUid)
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
				return TOptional.Empty<(UserBasic, UserPatronage, UserRecognition)>();
			}

			return TOptional.Success((query.userBasic, query.userPatronage, query.userRecognition));
		}
		catch(Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<(UserBasic, UserPatronage, UserRecognition)>(ex.Message);
		}
	}

	public async Task<TOptional<bool>> UpdateEncryptedUID(Int64 userUid, string encryptedUID)
	{
		try
		{
			var userBasic = await _context.UserBasics.FindAsync(userUid);
			if (userBasic == null)
			{
				return TOptional.Error<bool>("User not found");
			}

			userBasic.EncryptedUID = encryptedUID;
			//userBasic.DateModified = _timeProvider.GetUtcNow().UtcDateTime;

			return TOptional.Success(true);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOptional.Error<bool>(ex.Message);
		}
	}	
}