namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record RandomSendMsgCommand(Int64 UserUID) : IRequest<TOptional<SendMessageResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class RandomSendMsgCommandHandler : IRequestHandler<RandomSendMsgCommand, TOptional<SendMessageResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public RandomSendMsgCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOptional<SendMessageResponse>> Handle(RandomSendMsgCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.HasValue)
        {
            return TOptional.Error<SendMessageResponse>("Not found user.");
        }

        var response = new SendMessageResponse(0);

        return TOptional.Success(response);
    }
}