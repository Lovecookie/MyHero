
using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;

public class CreateUserMappingProfile : Profile
{
	public CreateUserMappingProfile()
	{
		CreateMap<CreateUserRequest, CreateUserCommand>()
			.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PicUrl))
			.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Pw));

		CreateMap<CreateUserCommand, UserBasic>();
	}
}