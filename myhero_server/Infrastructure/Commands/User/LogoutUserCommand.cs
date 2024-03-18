
namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
public class LogoutUserCommand(ClaimsPrincipal principal) : IRequest<OptBool>
{    
	public ClaimsPrincipal Principal { get; init; } = principal;
}

/// <summary>
/// 
/// </summary>
public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, OptBool>
{
	private readonly IUserAuthJwtRepository _userAuthJwtRepository;

	public LogoutUserCommandHandler(IUserAuthJwtRepository userAuthJwtRepository )
	{
		_userAuthJwtRepository = userAuthJwtRepository;		
	}

	public async Task<OptBool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
	{		
		if( !request.Principal.IsAuthenticated() )
		{
			return OptBool.Error("User is not authenticated.");
		}

		var userUID = request.Principal.GetUxt();
		var removeJWT = "";

		var bResult = await _userAuthJwtRepository.UpdateRefreshToken(userUID, removeJWT, removeJWT);
		if ( !bResult)
		{
			return OptBool.Error("Failed to remove refresh token.");
		}

		bResult = await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		if (!bResult)
		{
			return OptBool.Error("Failed to save user.");
		}

		return OptBool.Success();
	}
}