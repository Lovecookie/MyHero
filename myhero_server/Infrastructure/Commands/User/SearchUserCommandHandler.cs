
using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;

public class SearchUserCommandHandler : IRequestHandler<SearchUserCommand, TOptional<SearchUserResponse>>
{	
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMapper _mapper;

	public SearchUserCommandHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
	{
		_userBasicRepository = userBasicRepository;
		_mapper = mapper;
	}

	public async Task<TOptional<SearchUserResponse>> Handle(SearchUserCommand request, CancellationToken cancellationToken)
	{
		var opt = await _userBasicRepository.FindById(request.SearchWord);
		if(!opt.HasValue)
		{
			return TOptional.Error<SearchUserResponse>("Not found user.");
		}

		var response = new SearchUserResponse(opt.Value!.UserId, opt.Value!.PictureUrl);

		return TOptional.To(response);
	}
}