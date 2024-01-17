

using myhero_dotnet.Infrastructure.Constants;
using myhero_dotnet.Infrastructure.Enum;

namespace myhero_dotnet.Account.Requests;


public record SearchUserRequest
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}