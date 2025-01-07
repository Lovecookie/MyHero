namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchUserByStringCommand(string SearchWord, EUserSearchType SearchType) : IRequest<TOutcome<SearchUserResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchUserByStringCommandHandler : IRequestHandler<SearchUserByStringCommand, TOutcome<SearchUserResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchUserByStringCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOutcome<SearchUserResponse>> Handle(SearchUserByStringCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.FindById(request.SearchWord);
        if (!opt.Success)
        {
            return TOutcome.Err<SearchUserResponse>("Not found user.");
        }

        var response = new SearchUserResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOutcome.Ok(response);
    }
}