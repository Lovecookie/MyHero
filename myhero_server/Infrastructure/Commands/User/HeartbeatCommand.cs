namespace myhero_dotnet.Infrastructure;
public class HeartbeatCommand : IRequest<string>
{
    public string HeartBeat { get; set; } = "";
}