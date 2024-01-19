

using AutoMapper;
using myhero_dotnet.Account.Responses;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.DatabaseCore.Repositories;
using myhero_dotnet.Infrastructure.Commands.User;

public class SearchUserCommandHandler : IRequestHandler<SearchUserCommand, SearchUserResponse>
{	
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IMapper _mapper;

	public SearchUserCommandHandler(IUserBasicRepository userBasicRepository, IMapper mapper)
	{
		_userBasicRepository = userBasicRepository;
		_mapper = mapper;
	}

	public async Task<SearchUserResponse> Handle(SearchUserCommand request, CancellationToken cancellationToken)
	{
		var userEntity = _mapper.Map<UserBasic>(request);

		_userBasicRepository.FindAsync(request) 

		// return await _userBasicRepository.CreateAsync(userEntity, cancellationToken);
	}
}