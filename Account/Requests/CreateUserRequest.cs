

using myhero_dotnet.Infrastructure.Constants;

namespace myhero_dotnet.Account.Requests;


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