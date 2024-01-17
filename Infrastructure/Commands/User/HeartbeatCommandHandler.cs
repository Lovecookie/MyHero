using shipcret_server_dotnet.DatabaseCore.Entities;
using shipcret_server_dotnet.DatabaseCore.Repositories;
using shipcret_server_dotnet.Infrastructure.Commands;

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