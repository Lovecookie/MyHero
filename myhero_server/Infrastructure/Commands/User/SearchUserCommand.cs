using myhero_dotnet.Account.Responses;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.Infrastructure.Constants;
using myhero_dotnet.Infrastructure.Enum;

namespace myhero_dotnet.Infrastructure.Commands.User;


public class SearchUserCommand : IRequest<TOptional<Responses>>
{
	[Required]
	public string SearchWord { get; init; } = "";

	[Required]
	public EUserSearchType SearchType { get; init; } = EUserSearchType.None;
}