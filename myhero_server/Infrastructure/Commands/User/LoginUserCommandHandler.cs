namespace myhero_dotnet.Infrastructure;
public class LoginUserHandler : IRequestHandler<LoginUserCommand, TOptional<UserBasic>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMapper _mapper;

	public LoginUserHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
	{
		_userBasicRepository = userBasicRepository;
		_mapper = mapper;
	}

	public async Task<TOptional<UserBasic>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		// request.Email
		// request.Password
		var opt = await _userBasicRepository.FindByEmailAsync(request.Email);
		if(!opt.HasValue)
		{
			return TOptional.Error<UserBasic>("User not found");
		}

		if(request.Password != opt.Value!.Password)
		{
			return TOptional.Error<UserBasic>("Invalid password");
		}

		return TOptional.To(opt.Value!);
	}
}