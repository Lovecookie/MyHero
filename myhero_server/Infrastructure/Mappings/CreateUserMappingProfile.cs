
using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;

public class CreateUserMappingProfile : Profile
{
	public CreateUserMappingProfile()
	{
		CreateMap<CreateUserRequest, CreateUserCommand>()			
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Pw));

		CreateMap<CreateUserRequest, LoginUserRequest>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.Pw, opt => opt.MapFrom(src => src.Pw));

		CreateMap<CreateUserCommand, UserBasic>();
	}
}