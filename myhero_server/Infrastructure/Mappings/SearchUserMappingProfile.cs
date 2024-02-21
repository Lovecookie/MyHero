using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;

public class SearchUserMappingProfile : Profile
{
	public SearchUserMappingProfile()
	{
		CreateMap<SearchUserRequest, SearchUserCommand>();
			//.ForMember(dest => dest.SearchWord, opt => opt.MapFrom(src => src.SearchWord))
	}
}