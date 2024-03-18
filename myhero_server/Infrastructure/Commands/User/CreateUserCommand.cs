
namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
public class CreateUserCommand : IRequest<TOptional<TokenInfo>>
{
    public string UserId { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string PictureUrl { get; set; } = "";
}

/// <summary>
/// 
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, TOptional<TokenInfo>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public CreateUserHandler(IUserBasicRepository userBasicRepository, IMediator mediator, IMapper mapper )
	{
		_userBasicRepository = userBasicRepository;
		_mediator = mediator;
		_mapper = mapper;
	}

	public async Task<TOptional<TokenInfo>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var userEntity = _mapper.Map<UserBasic>(request);

		var createOpt = await _userBasicRepository.Create(userEntity, cancellationToken);
		if( !createOpt.HasValue)
		{
			return TOptional.Error<TokenInfo>(createOpt.Message);
		}

		var bResult = await _userBasicRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		if (!bResult)
		{
			return TOptional.Error<TokenInfo>("Failed to save user.");
		}

		var accessJwtOpt = await _mediator.Send(new AccessJwtCommand(createOpt.Value!.UserUID, createOpt.Value!.Email));
		if (!accessJwtOpt.HasValue)
		{
			return TOptional.Error<TokenInfo>(accessJwtOpt.Message);
		}

		return TOptional.Success(accessJwtOpt.Value!);
	}
}