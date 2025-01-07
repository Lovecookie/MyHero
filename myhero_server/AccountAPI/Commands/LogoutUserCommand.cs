namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
public record LogoutUserCommand(ClaimsPrincipal Principal) : IRequest<TOutcome<bool>>
{
}

/// <summary>
/// 
/// </summary>
public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, TOutcome<bool>>
{
    private readonly IUserAuthJwtRepository _userAuthJwtRepository;

    public LogoutUserCommandHandler(IUserAuthJwtRepository userAuthJwtRepository)
    {
        _userAuthJwtRepository = userAuthJwtRepository;
    }

    public async Task<TOutcome<bool>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var uid = await request.Principal.TryDecryptUID();
        if (!uid.Success)
        {
            return TOutcome.Err<bool>("User is not authenticated.");
        }

        var removeJWT = "";
        var bResult = await _userAuthJwtRepository.UpdateRefreshToken(uid.Value, removeJWT, removeJWT);
        if (!bResult)
        {
            return TOutcome.Err<bool>("Failed to remove refresh token.");
        }

        bResult = await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        if (!bResult)
        {
            return TOutcome.Err<bool>("Failed to save user.");
        }

        return TOutcome.Ok(true);
    }
}