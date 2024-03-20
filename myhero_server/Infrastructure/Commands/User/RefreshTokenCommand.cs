namespace myhero_dotnet.Infrastructure;


/// <summary>
/// 
/// </summary>
public record RefreshTokenCommand(string Email, string Password) : IRequest<TOptional<UserBasic>>
{
}

/// <summary>
/// 
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TOptional<UserBasic>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMapper _mapper;

	public RefreshTokenCommandHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
	{
		_userBasicRepository = userBasicRepository;
		_mapper = mapper;
	}

	public async Task<TOptional<UserBasic>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var opt = await _userBasicRepository.FindByEmail(request.Email);
		if (!opt.HasValue)
		{
			return TOptional.Error<UserBasic>("User not found");
		}

		if (request.Password != opt.Value!.Password)
		{
			return TOptional.Error<UserBasic>("Invalid password");
		}

		return TOptional.Success(opt.Value!);
	}
}