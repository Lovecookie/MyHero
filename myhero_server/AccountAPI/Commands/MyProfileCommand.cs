namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
public record MyProfileCommand(ClaimsPrincipal Principal) : IRequest<TOutcome<MyProfileResponse>>
{
}

/// <summary>
/// 
/// </summary>
public class MyProfileCommandHandler : IRequestHandler<MyProfileCommand, TOutcome<MyProfileResponse>>
{
    //private readonly AccountDBContext _context;
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MyProfileCommandHandler(IUserBasicRepository userBasicRepository, IMediator mediator, IMapper mapper)
    {
        _userBasicRepository = userBasicRepository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<TOutcome<MyProfileResponse>> Handle(MyProfileCommand request, CancellationToken cancellationToken)
    {
        var uidOpt = await request.Principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOutcome.Error<MyProfileResponse>("Unauthorized");
        }

        var queryOpt = await _userBasicRepository.SelectUserBasic(uidOpt.Value);
        if (!queryOpt.HasValue)
        {
            return TOutcome.Error<MyProfileResponse>(queryOpt.Message);
        }

        var userBasicTuple = queryOpt.Value;
        return TOutcome.Success(
            new MyProfileResponse(
                PicURL: userBasicTuple.Item1.PictureUrl,
                FollowersCount: userBasicTuple.Item2.FollowerCount,
                FollowingCount: userBasicTuple.Item2.FollowingCount,
                Posts: 0,
                FamousValue: userBasicTuple.Item3.FamousValue
                )
            );
    }
}