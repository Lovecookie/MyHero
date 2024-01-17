
using AutoMapper;
using myhero_dotnet.Account.Requests;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.Infrastructure.Commands.User;

public class SearchUserMappingProfile : Profile
{
	public SearchUserMappingProfile()
	{
		CreateMap<SearchUserRequest, SearchUserCommand>();
			//.ForMember(dest => dest.SearchWord, opt => opt.MapFrom(src => src.SearchWord))
	}
}