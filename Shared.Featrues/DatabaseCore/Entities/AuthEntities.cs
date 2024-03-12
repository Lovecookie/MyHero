

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
public class UserAuthJwt : UserEntityBase<UserAuthJwt>, IAggregateRoot
{ 
    public string AccessToken { get; set; } = ""; 
    
    public string RefreshToken { get; set; } = "";
}

