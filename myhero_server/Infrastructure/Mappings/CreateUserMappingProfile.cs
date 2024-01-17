
using AutoMapper;
using myhero_dotnet.Account.Requests;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.Infrastructure.Commands.User;

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