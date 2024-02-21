

namespace myhero_dotnet.Account;


/// <summary>
/// Response for creating a user
/// </summary>
public record SearchUserResponse
{
    [Required]
    public string? UserId { get; init; }

    [Required]
    public string? PicUrl { get; init; }
}


/// <summary>
/// created token 
/// </summary>
public record AuthenticatedResponse
{
    public string Token { get; set; } = "";
}

