namespace myhero_dotnet.Account;


public record CreateUserRequest
{
	[Required]
	public string? UserId { get; init; }

	[Required]
	public string? Email { get; init; }

	[Required]
	public string? Pw { get; init; }	
}

public record LoginUserRequest
{
	[Required]
	public string? Email { get; init; }

	[Required]
	public string? Pw { get; init; }
}

public record AccessTokenRequest()
{
	[Required]
	public Int64 UserUID { get; init; }
}

public record HeartbeatRequest
{
	[JsonPropertyName("heatbeat")]
	public string Heatbeat { get; init; } = "";
}

public record SearchUserRequest
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}

public record RefreshTokenRequest
{
	[Required]
	public string Email { get; init; } = "";

	[Required]
	public string Pw { get; init; } = "";
}

