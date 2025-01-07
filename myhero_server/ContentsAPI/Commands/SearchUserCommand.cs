namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchUserCommand(Int64 UserUID) : IRequest<TOutcome<SearchUserResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchUserCommandHandler : IRequestHandler<SearchUserCommand, TOutcome<SearchUserResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchUserCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOutcome<SearchUserResponse>> Handle(SearchUserCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.Success)
        {
            return TOutcome.Err<SearchUserResponse>("Not found user.");
        }

        var response = new SearchUserResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOutcome.Ok(response);
    }
}