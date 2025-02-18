﻿namespace myhero_dotnet.ContentsAPI;


/// <summary>
/// 
/// </summary>
public record SearchGroupCommand(Int64 UserUID) : IRequest<TOutcome<SearchGroupResponse>>
{
}


/// <summary>
/// 
/// </summary>
public class SearchGroupCommandHandler : IRequestHandler<SearchGroupCommand, TOutcome<SearchGroupResponse>>
{
    private readonly IUserBasicRepository _userBasicRepository;    

    public SearchGroupCommandHandler(IUserBasicRepository userBasicRepository)
    {
        _userBasicRepository = userBasicRepository;        
    }

    public async Task<TOutcome<SearchGroupResponse>> Handle(SearchGroupCommand request, CancellationToken cancellationToken)
    {
        var opt = await _userBasicRepository.Find(request.UserUID);
        if (!opt.Success)
        {
            return TOutcome.Err<SearchGroupResponse>("Not found user.");
        }

        var response = new SearchGroupResponse(opt.Value!.UserID, opt.Value!.PictureUrl);

        return TOutcome.Ok(response);
    }
}