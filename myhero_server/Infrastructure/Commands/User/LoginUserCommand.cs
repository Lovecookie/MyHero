namespace myhero_dotnet.Infrastructure;

public class LoginUserCommand : IRequest<TOptional<UserBasic>>
{
	public string Email { get; set; } = "";

	public string Password { get; set; } = "";
}