namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SendHowlCommand(Int64 UserUID) : IRequest<TOptional<SendHowlResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SendHowlCommandHandler : IRequestHandler<SendHowlCommand, TOptional<SendHowlResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SendHowlCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOptional<SendHowlResponse>> Handle(SendHowlCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.HasValue)
        {
            return TOptional.Error<SendHowlResponse>("Not found user.");
        }

        var response = new SendHowlResponse(0);

        return TOptional.Success(response);
    }
}