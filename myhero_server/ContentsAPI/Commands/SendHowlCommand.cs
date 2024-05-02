namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SendHowlCommand(ClaimsPrincipal Principal, string HowlMessage) : IRequest<TOptional<SendHowlResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SendHowlCommandHandler : IRequestHandler<SendHowlCommand, TOptional<SendHowlResponse>>
{
    private readonly IHowlMessageRepository _howlMessageRepository;

    public SendHowlCommandHandler(IHowlMessageRepository howlMessageRepository)
    {
        _howlMessageRepository = howlMessageRepository;
    }

    public async Task<TOptional<SendHowlResponse>> Handle(SendHowlCommand request, CancellationToken cancellationToken)
    {
        var uidOpt = await request.Principal.TryDecryptUID();
        if (!uidOpt.HasValue)
        {
            return TOptional.Error<SendHowlResponse>(uidOpt.Message);
        }

        var howlMessage = new UserHowl
        {
            UserUID = uidOpt.Value,
            Message = request.HowlMessage,
        };

        var bResult = await _howlMessageRepository.Create(howlMessage);


        return TOptional.Success(new SendHowlResponse());
    }
}