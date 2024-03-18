using myhero_dotnet.Account;
using myhero_dotnet.EventBus;

namespace myhero_dotnet.Infrastructure;


/// <summary>
/// 
/// </summary>
public class SignInCommand(string email, string password) : IRequest<TOptional<SignInResponse>>
{
	public string Email { get; init; } = email;

	public string Password { get; init; } = password;
}

/// <summary>
/// 
/// </summary>
public class SignInCommandHandler : IRequestHandler<SignInCommand, TOptional<SignInResponse>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMediator _mediator;

	public SignInCommandHandler(IUserBasicRepository userBasicRepository, IMediator mediator)
	{
		_userBasicRepository = userBasicRepository;
		_mediator = mediator;
	}

	public async Task<TOptional<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
	{
		var userBasicOpt = await _userBasicRepository.FindByEmail(request.Email);
		if (!userBasicOpt.HasValue)
		{
			return TOptional.Error<SignInResponse>("User not found");
		}

		if (request.Password != userBasicOpt.Value!.Password)
		{
			return TOptional.Error<SignInResponse>("Invalid password");
		}

		var accessJwtOpt = await _mediator.Send(new AccessJwtCommand(userBasicOpt.Value.UserUID, userBasicOpt.Value.Email));
		if (!accessJwtOpt.HasValue)
		{ 
			return TOptional.Error<SignInResponse>(accessJwtOpt.Message);
		}

		return TOptional.Success(new SignInResponse(userBasicOpt.Value.UserID, userBasicOpt.Value.Email, accessJwtOpt.Value!));
	}
}