

namespace myhero_dotnet.Infrastructure.Commands;

public class HeartbeatCommand : IRequest<string>
{
	public string HeartBeat { get; set; } = "";
}