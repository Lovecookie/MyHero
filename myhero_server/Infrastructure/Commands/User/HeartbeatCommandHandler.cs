using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.DatabaseCore.Repositories;
using myhero_dotnet.Infrastructure.Commands.User;

public class HeartbeatCommandHandler : IRequestHandler<HeartbeatCommand, string>
{
	public HeartbeatCommandHandler()
	{
	}

	public async Task<string> Handle(HeartbeatCommand request, CancellationToken cancellationToken)
	{
		return await Task<string>.FromResult(request.HeartBeat!);
	}
}