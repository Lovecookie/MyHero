namespace myhero_dotnet.Account.Requests;

public record LoginUserRequest
{
    [Required, JsonPropertyName("email")]
    public string? Email { get; init; }

    [Required, JsonPropertyName("password")]
    public string? Password { get; init; }
}

