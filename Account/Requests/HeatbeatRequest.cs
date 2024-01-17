namespace myhero_dotnet.Account.Requests;

public record HeartbeatRequest
{
	[JsonPropertyName("heatbeat")]
	public string Heatbeat { get; init; } = "";
}

