namespace myhero_dotnet.Infrastructure;
[DataContract]
public class TestCommand : IRequest<bool>
{
    [DataMember]
    public required string TestString { get; set; }
}
