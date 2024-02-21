using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;


public class SearchUserCommand : IRequest<TOptional<SearchUserResponse>>
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}