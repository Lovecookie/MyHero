﻿namespace myhero_dotnet.Infrastructure;

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