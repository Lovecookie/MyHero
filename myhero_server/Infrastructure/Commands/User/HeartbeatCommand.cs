namespace myhero_dotnet.Infrastructure.Commands.User;

public class HeartbeatCommand : IRequest<string>
{
    public string HeartBeat { get; set; } = "";
}