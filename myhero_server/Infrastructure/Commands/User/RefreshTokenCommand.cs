namespace myhero_dotnet.Infrastructure;

public class RefreshTokenCommand : IRequest<TOptional<UserBasic>>
{
	public string Email { get; set; } = "";

	public string Password { get; set; } = "";
}