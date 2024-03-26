namespace myhero_dotnet.AccountAPI;
public record HeartbeatCommand(string Heartbeat) : IRequest<string>
{
}

public class HeartbeatCommandHandler : IRequestHandler<HeartbeatCommand, string>
{
    public HeartbeatCommandHandler()
    {
    }

    public async Task<string> Handle(HeartbeatCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Heartbeat!);
    }
}