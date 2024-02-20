

namespace Shared.Features.Commands;

public class AuthenticateJwtCommand : IRequest<TOptional<string>>
{
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}