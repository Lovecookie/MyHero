
namespace myhero_dotnet.AccountAPI;


/// <summary>
/// 
/// </summary>
public record SignInCommand(string Email, string Password) : IRequest<TOutcome<SignInResponse>>
{
}

/// <summary>
/// 
/// </summary>
public class SignInCommandHandler : IRequestHandler<SignInCommand, TOutcome<SignInResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IMediator _mediator;

    public SignInCommandHandler(IUserBasicRepository userBasicRepository, IMediator mediator)
    {
        _userBasicRepository = userBasicRepository;
        _mediator = mediator;
    }

    public async Task<TOutcome<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var userBasicOpt = await _userBasicRepository.FindByEmail(request.Email);
        if (!userBasicOpt.Success)
        {
            return TOutcome.Err<SignInResponse>("User not found");
        }

        if (request.Password != userBasicOpt.Value!.Password)
        {
            return TOutcome.Err<SignInResponse>("Invalid password");
        }

        var accessJwtOpt = await _mediator.Send(new AccessJwtCommand(userBasicOpt.Value.UserUID, userBasicOpt.Value.Email));
        if (!accessJwtOpt.Success)
        {
            return TOutcome.Err<SignInResponse>(accessJwtOpt.Message);
        }

        return TOutcome.Ok(new SignInResponse(userBasicOpt.Value.UserID, userBasicOpt.Value.Email, accessJwtOpt.Value!));
    }
}