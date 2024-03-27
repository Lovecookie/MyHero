namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchUserByStringCommand(string SearchWord, EUserSearchType SearchType) : IRequest<TOptional<SearchUserResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchUserByStringCommandHandler : IRequestHandler<SearchUserByStringCommand, TOptional<SearchUserResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchUserByStringCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOptional<SearchUserResponse>> Handle(SearchUserByStringCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.FindById(request.SearchWord);
        if (!opt.HasValue)
        {
            return TOptional.Error<SearchUserResponse>("Not found user.");
        }

        var response = new SearchUserResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOptional.Success(response);
    }
}