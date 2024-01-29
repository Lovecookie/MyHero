using myhero_dotnet.CommonFeatures.GenericObjects;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.Infrastructure.Constants;

namespace myhero_dotnet.Infrastructure.Commands.User;


public class CreateUserCommand : IRequest<TOptional<UserBasic>>
{
    public string UserId { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public string PictureUrl { get; set; } = "";
}