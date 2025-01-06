using myhero_dotnet.Infrastructure;
using Shared.Featrues.Algorithm;

namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
public record CreateUserCommand(string UserID, string Email, string Password, string PictureURL) : IRequest<TOutcome<TokenInfo>>
{
}

/// <summary>
/// 
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, TOutcome<TokenInfo>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserBasicRepository userBasicRepository, IMediator mediator, IMapper mapper)
    {
        _userBasicRepository = userBasicRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<TOutcome<TokenInfo>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = _mapper.Map<UserBasic>(request);

        var createOpt = await _userBasicRepository.Create(userEntity);
        if (!createOpt.HasValue)
        {
            return TOutcome.Error<TokenInfo>(createOpt.Message);
        }

        var bResult = await _userBasicRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        if (!bResult)
        {
            return TOutcome.Error<TokenInfo>("Failed to save user.");
        }

        var encryptUID = await AesWrapper.EncryptAsString(createOpt.Value!.UserUID);
		if (encryptUID == null)
        {
            return TOutcome.Error<TokenInfo>("Failed to encrypt UID.");
		}

        var updateOpt = await _userBasicRepository.UpdateEncryptedUID(createOpt.Value!.UserUID, encryptUID);
        if (!updateOpt.HasValue)
        {
			return TOutcome.Error<TokenInfo>(updateOpt.Message);
		}

        bResult = await _userBasicRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        if (!bResult)
        {
			return TOutcome.Error<TokenInfo>("Failed to save user.");
		}

        var accessJwtOpt = await _mediator.Send(new AccessJwtCommand(createOpt.Value!.UserUID, createOpt.Value!.Email));
        if (!accessJwtOpt.HasValue)
        {
            return TOutcome.Error<TokenInfo>(accessJwtOpt.Message);
        }

        return TOutcome.Success(accessJwtOpt.Value!);
    }
}