namespace myhero_dotnet.ContentsAPI;


public record SearchUserRequest
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}

public record SearchGroupRequest
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EGroupSearchType SearchType { get; init; } = EGroupSearchType.None;
}

public record SendHowlRequest
{
	[Required]
	public string Message { get; init; } = "";
}