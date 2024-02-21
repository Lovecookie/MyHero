
namespace myhero_dotnet.Infrastructure;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, TOptional<UserBasic>>
{	
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMapper _mapper;

	public CreateUserHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
	{
		_userBasicRepository = userBasicRepository;
		_mapper = mapper;
	}

	public async Task<TOptional<UserBasic>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var userEntity = _mapper.Map<UserBasic>(request);

		return await _userBasicRepository.CreateAsync(userEntity, cancellationToken);
	}
}