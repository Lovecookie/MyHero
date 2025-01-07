

namespace Shared.Features.DatabaseCore;

public interface IUserAuthJwtRepository : IDefaultRepository<UserAuthJwt>
{
	Task<TOutcome<UserBasic>> FindAsync(Int64 userUid);
	Task<bool> Add(UserAuthJwt userAuthJwt);
	Task<bool> UpdateAccessToken(Int64 userUid, string accessToken);
	Task<bool> UpdateRefreshToken(Int64 userUid, string accessToken, string refreshToken);
}

public class UserAuthJwtRepository : IUserAuthJwtRepository
{
	private readonly AuthDBContext _context;
	private readonly ILogger _logger;
	private readonly TimeProvider _timeProvider;

	public IUnitOfWork UnitOfWork => _context;
	

	public UserAuthJwtRepository(AuthDBContext context, ILogger<UserAuthJwtRepository> logger, TimeProvider timeProvider)
	{ 
		_timeProvider = timeProvider;
		_context = context;
		_logger = logger;
	}

	public async Task<TOutcome<UserBasic>> FindAsync(Int64 userUid)
	{
		try
		{
			var entity = await _context.FindAsync<UserBasic>(new { UserUid = userUid });
			if(entity == null)
			{
				return TOutcome.Empty<UserBasic>();
			}

			return TOutcome.Ok(entity);
		}

		catch (Exception ex)
		{
			_logger.LogError(ex.Message);

			return TOutcome.Err<UserBasic>("Not found");
		}
	}

	public async Task<bool> Add(UserAuthJwt userAuthJwt)
	{
		try
		{
			var existing = await _context.UserAuthJwts.FirstOrDefaultAsync(e => e.UserUID == userAuthJwt.UserUID);
			if(existing != null)
			{
				_context.UserAuthJwts.Entry(existing).CurrentValues.SetValues(userAuthJwt);
			}
			else
			{
				var entity = await _context.UserAuthJwts.AddAsync(userAuthJwt);
				if( entity == null )
				{
					return false;
				}
			}
		}		
		catch(DbUpdateException ex)
		{
			_logger.LogError(ex.Message);
			return false;
		}

		return true;
	}

	public async Task<bool> UpdateAccessToken(Int64 userUid, string accessToken)
	{
		try
		{
			var existing = await _context.UserAuthJwts.FirstOrDefaultAsync(e => e.UserUID == userUid);
			if(existing != null)
			{
				_context.UserAuthJwts.Entry(existing).Entity.AccessToken = accessToken;
			}
			else
			{
				return false;
			}
		}
		catch(DbUpdateException ex)
		{
			_logger.LogError(ex.Message);

			return false;
		}

		return true;
	}

	public async Task<bool> UpdateRefreshToken(Int64 userUid, string accessToken, string refreshToken)
	{
		try
		{
			var existing = await _context.UserAuthJwts.FirstOrDefaultAsync(e => e.UserUID == userUid);
			if (existing != null)
			{
				_context.UserAuthJwts.Entry(existing).Entity.AccessToken = accessToken;
				_context.UserAuthJwts.Entry(existing).Entity.RefreshToken = refreshToken;
			}
			else
			{
				return false;
			}
		}
		catch (DbUpdateException ex)
		{
			_logger.LogError(ex.Message);
			return false;
		}
		catch(Exception ex)
		{
			_logger.LogError(ex.Message);
			return false;
		}

		return true;
	}

	/* multiple added
	 * ref : https://github.com/dotnet/efcore/issues/24780
		foreach (var item in items)
		{
			var existing = context.AList.Local.FirstOrDefault(e => e.Id == item.Id);
			if (existing != null)
			{
				context.SaveChanges();
				existing.A = item.A;
			}
			else
			{
				context.Add(item);
			}
		}

		context.SaveChanges();
	*/
}