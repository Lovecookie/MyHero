
using AutoMapper;
using shipcret_server_dotnet.Account.Requests;
using shipcret_server_dotnet.DatabaseCore.Entities;
using shipcret_server_dotnet.Infrastructure.Commands;

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