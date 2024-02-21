
namespace myhero_dotnet.Infrastructure;

public class CreateUserCommand : IRequest<TOptional<UserBasic>>
{
    public string UserId { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string PictureUrl { get; set; } = "";
}