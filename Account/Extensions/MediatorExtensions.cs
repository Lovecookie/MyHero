using shipcret_server_dotnet.DatabaseCore.DbContexts;
using System.Runtime.CompilerServices;

namespace shipcret_server_dotnet.Account.Extensions;

public static class MediatorExtensions
{
	public static async Task DispatchDomainEventAsync(this IMediator mediator, AccountDbContext context)
	{
		
	}
}
