
namespace myhero_dotnet.ContentsAPI;



public record SearchUserResponse(string UserID, string PicURL)
{ }

public record SearchGroupResponse(string GroupID, string PicURL)
{ }