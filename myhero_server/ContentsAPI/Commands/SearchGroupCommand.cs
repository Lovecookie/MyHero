namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchGroupCommand(Int64 UserUID) : IRequest<TOptional<SearchGroupResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchGroupCommandHandler : IRequestHandler<SearchGroupCommand, TOptional<SearchGroupResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchGroupCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOptional<SearchGroupResponse>> Handle(SearchGroupCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.HasValue)
        {
            return TOptional.Error<SearchGroupResponse>("Not found user.");
        }

        var response = new SearchGroupResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOptional.Success(response);
    }
}