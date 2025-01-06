namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
public record RefreshTokenCommand(string Email, string Password) : IRequest<TOutcome<UserBasic>>
{
}

/// <summary>
/// 
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TOutcome<UserBasic>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
    {
        _userBasicRepository = userBasicRepository;
        _mapper = mapper;
    }

    public async Task<TOutcome<UserBasic>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.FindByEmail(request.Email);
        if (!opt.HasValue)
        {
            return TOutcome.Error<UserBasic>("User not found");
        }

        if (request.Password != opt.Value!.Password)
        {
            return TOutcome.Error<UserBasic>("Invalid password");
        }

        return TOutcome.Success(opt.Value!);
    }
}