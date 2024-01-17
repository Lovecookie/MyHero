

using shipcret_server_dotnet.DatabaseCore.Entities;
using shipcret_server_dotnet.Infrastructure.Constants;

namespace shipcret_server_dotnet.Infrastructure.Commands;


public class CreateUserCommand : IRequest<UserBasic>
{	
	public string UserId { get; set; } = "";
	
	public string Email { get; set; } = "";
	
	public string Password { get; set; } = "";
	
	public string PictureUrl { get; set; } = "";
}