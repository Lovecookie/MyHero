

using shipcret_server_dotnet.Infrastructure.Constants;

namespace shipcret_server_dotnet.Account.Requests;


public record CreateUserRequest
{
    [Required]
    public string? UserId { get; init; }

    [Required]
    public string? Email { get; init; }

    [Required]
    public string? Pw { get; init; }

    [Required]
    public string? PicUrl { get; init; }

}