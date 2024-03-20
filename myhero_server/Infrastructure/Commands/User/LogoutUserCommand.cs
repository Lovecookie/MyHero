
namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
public record LogoutUserCommand(ClaimsPrincipal Principal) : IRequest<TOptional<bool>>
{   
}

/// <summary>
/// 
/// </summary>
public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, TOptional<bool>>
{
	private readonly IUserAuthJwtRepository _userAuthJwtRepository;

	public LogoutUserCommandHandler(IUserAuthJwtRepository userAuthJwtRepository )
	{
		_userAuthJwtRepository = userAuthJwtRepository;		
	}

	public async Task<TOptional<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
	{		
		var principal = request.Principal;
		if( !principal.IsAuthenticated() )
		{
			return TOptional.Error<bool>("User is not authenticated.");
		}
		
		var uid = await principal.DecryptUID();
		if( !uid.HasValue )
		{
			return TOptional.Error<bool>("User is not authenticated.");
		}

		var removeJWT = "";
		var bResult = await _userAuthJwtRepository.UpdateRefreshToken(uid.Value, removeJWT, removeJWT);
		if ( !bResult)
		{
			return TOptional.Error<bool>("Failed to remove refresh token.");
		}

		bResult = await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		if (!bResult)
		{
			return TOptional.Error<bool>("Failed to save user.");
		}

		return TOptional.Success(true);
	}
}