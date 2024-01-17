

namespace myhero_dotnet.Infrastructure.Enum;


[Flags]
public enum EUserSearchType : byte
{
	None = 0,
	Id = 0b_0000_0001, // 1
	Name = 0b_0000_0010, // 2	

	Max,
}