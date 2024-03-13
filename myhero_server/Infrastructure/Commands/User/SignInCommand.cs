namespace myhero_dotnet.Infrastructure;


/// <summary>
/// 
/// </summary>
public class SignInCommand(string email, string password) : IRequest<TOptional<(UserBasic, TokenInfo)>>
{
	public string Email { get; init; } = email;

	public string Password { get; init; } = password;
}

/// <summary>
/// 
/// </summary>
public class SignInCommandHandler : IRequestHandler<SignInCommand, TOptional<(UserBasic, TokenInfo)>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMediator _mediator;

	public SignInCommandHandler(IUserBasicRepository userBasicRepository, IMediator mediator)
	{
		_userBasicRepository = userBasicRepository;
		_mediator = mediator;
	}

	public async Task<TOptional<(UserBasic, TokenInfo)>> Handle(SignInCommand request, CancellationToken cancellationToken)
	{
		var userBasicOpt = await _userBasicRepository.FindByEmail(request.Email);
		if (!userBasicOpt.HasValue)
		{
			return TOptional.Error<(UserBasic, TokenInfo)>("User not found");
		}

		if (request.Password != userBasicOpt.Value!.Password)
		{
			return TOptional.Error<(UserBasic, TokenInfo)>("Invalid password");
		}

		var accessJwtOpt = await _mediator.Send(new AccessJwtCommand(userBasicOpt.Value.UserUID, userBasicOpt.Value.Email));
		if (!accessJwtOpt.HasValue)
		{ 
			return TOptional.Error<(UserBasic, TokenInfo)>(accessJwtOpt.Message);
		}

		return TOptional.To((userBasicOpt.Value!, accessJwtOpt.Value!));
	}
}