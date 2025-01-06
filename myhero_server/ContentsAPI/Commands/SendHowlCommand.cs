namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SendHowlCommand(ClaimsPrincipal Principal, string HowlMessage) : IRequest<TOutcome<SendHowlResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SendHowlCommandHandler : IRequestHandler<SendHowlCommand, TOutcome<SendHowlResponse>>
{
    private readonly IHowlMessageRepository _howlMessageRepository;

    public SendHowlCommandHandler(IHowlMessageRepository howlMessageRepository)
    {
        _howlMessageRepository = howlMessageRepository;
    }

    public async Task<TOutcome<SendHowlResponse>> Handle(SendHowlCommand request, CancellationToken cancellationToken)
    {
        var uidOpt = await request.Principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOutcome.Error<SendHowlResponse>(uidOpt.Message);
        }

        var howlMessage = new UserHowl
        {
            UserUID = uidOpt.Value,
            Message = request.HowlMessage,
        };

        var bResult = await _howlMessageRepository.Create(howlMessage);


        return TOutcome.Success(new SendHowlResponse());
    }
}