using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.Infrastructure.Constants;

namespace myhero_dotnet.Infrastructure.Commands.User;


public class RefreshTokenCommand : IRequest<TOptional<UserBasic>>
{
	public string Email { get; set; } = "";

	public string Password { get; set; } = "";
}