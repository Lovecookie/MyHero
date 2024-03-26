namespace myhero_dotnet.ContentsAPI;


public record SearchUserRequest
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}
