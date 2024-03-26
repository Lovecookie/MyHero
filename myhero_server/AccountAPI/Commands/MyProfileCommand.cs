namespace myhero_dotnet.AccountAPI;

/// <summary>
/// 
/// </summary>
public record MyProfileCommand(ClaimsPrincipal Principal) : IRequest<TOptional<MyProfileResponse>>
{
}

/// <summary>
/// 
/// </summary>
public class MyProfileCommandHandler : IRequestHandler<MyProfileCommand, TOptional<MyProfileResponse>>
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

    public async Task<TOptional<MyProfileResponse>> Handle(MyProfileCommand request, CancellationToken cancellationToken)
    {
        var principal = request.Principal;
        if (!principal.IsAuthenticated())
        {
            return TOptional.Error<MyProfileResponse>("Unauthorized");
        }

        var decryptedUID = await request.Principal.DecryptUID();
        if (!decryptedUID.HasValue)
        {
            return TOptional.Error<MyProfileResponse>("Unauthorized");
        }

        var queryOpt = await _userBasicRepository.SelectUserBasic(decryptedUID.Value);
        if (!queryOpt.HasValue)
        {
            return TOptional.Error<MyProfileResponse>(queryOpt.Message);
        }

        var userBasicTuple = queryOpt.Value;
        return TOptional.Success(
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