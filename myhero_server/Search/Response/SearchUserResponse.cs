

using myhero_dotnet.Infrastructure.Constants;

namespace myhero_dotnet.Account.Responses;


public record SearchUserResponse
{
    [Required]
    public string? UserId { get; init; }

    [Required]
    public string? PicUrl { get; init; }
}