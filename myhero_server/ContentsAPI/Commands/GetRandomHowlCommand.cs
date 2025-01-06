namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record GetRandomHowlCommand(ClaimsPrincipal Principal) : IRequest<TOutcome<GetHowlResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class GetRandomHowlCommandHandler : IRequestHandler<GetRandomHowlCommand, TOutcome<GetHowlResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IHowlMessageRepository _howlMessageRepository;

    public GetRandomHowlCommandHandler(IUserBasicRepository userBasicRepository, IHowlMessageRepository howlMessageRepository)
    {
        _userBasicRepository = userBasicRepository;
        _howlMessageRepository = howlMessageRepository;
    }

    public async Task<TOutcome<GetHowlResponse>> Handle(GetRandomHowlCommand request, CancellationToken cancellationToken)
    {
        var uidOpt = await request.Principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOutcome.Error<GetHowlResponse>(uidOpt.Message);
        }

        var howlOpt = await _howlMessageRepository.GetRandom(uidOpt.Value);
        if(!howlOpt.HasValue)
        {
            return TOutcome.Error<GetHowlResponse>(howlOpt.Message);
        }

        var userOpt = await _userBasicRepository.Find(howlOpt.Value!.UserUID);
        if(!userOpt.HasValue)
        {
            return TOutcome.Error<GetHowlResponse>(userOpt.Message);
        }        

        return TOutcome.Success(new GetHowlResponse(userOpt.Value!.EncryptedUID, userOpt.Value!.UserID, howlOpt.Value!.Message));
    }
}