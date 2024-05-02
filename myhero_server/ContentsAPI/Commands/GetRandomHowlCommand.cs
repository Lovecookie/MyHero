namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record GetRandomHowlCommand(ClaimsPrincipal Principal) : IRequest<TOptional<GetHowlResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class GetRandomHowlCommandHandler : IRequestHandler<GetRandomHowlCommand, TOptional<GetHowlResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;
    private readonly IHowlMessageRepository _howlMessageRepository;

    public GetRandomHowlCommandHandler(IUserBasicRepository userBasicRepository, IHowlMessageRepository howlMessageRepository)
    {
        _userBasicRepository = userBasicRepository;
        _howlMessageRepository = howlMessageRepository;
    }

    public async Task<TOptional<GetHowlResponse>> Handle(GetRandomHowlCommand request, CancellationToken cancellationToken)
    {
        var uidOpt = await request.Principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOptional.Error<GetHowlResponse>(uidOpt.Message);
        }

        var howlOpt = await _howlMessageRepository.GetRandom(uidOpt.Value);
        if(!howlOpt.HasValue)
        {
            return TOptional.Error<GetHowlResponse>(howlOpt.Message);
        }

        var userOpt = await _userBasicRepository.Find(howlOpt.Value!.UserUID);
        if(!userOpt.HasValue)
        {
            return TOptional.Error<GetHowlResponse>(userOpt.Message);
        }        

        return TOptional.Success(new GetHowlResponse(userOpt.Value!.EncryptedUID, userOpt.Value!.UserID, howlOpt.Value!.Message));
    }
}