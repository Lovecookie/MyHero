

namespace myhero_dotnet.Commands;

[DataContract]
public class TestCommand : IRequest<bool>
{
	[DataMember]
	public required string TestString { get; set; }
}
