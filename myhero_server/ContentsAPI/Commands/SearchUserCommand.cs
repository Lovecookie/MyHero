namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchUserCommand(Int64 UserUID) : IRequest<TOptional<SearchUserResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchUserCommandHandler : IRequestHandler<SearchUserCommand, TOptional<SearchUserResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchUserCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOptional<SearchUserResponse>> Handle(SearchUserCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.HasValue)
        {
            return TOptional.Error<SearchUserResponse>("Not found user.");
        }

        var response = new SearchUserResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOptional.Success(response);
    }
}