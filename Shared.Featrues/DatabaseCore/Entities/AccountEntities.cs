

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
[Index(nameof(UserID), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
public class UserBasic : UserEntityBase<UserBasic>, IAggregateRoot
{	
    public string EncryptedUID { get; set; } = "";
	public string UserID { get; set; } = ""; 
    
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    public string PictureUrl { get; set; } = "";
}


/// <summary>
/// UserPatronage
/// </summary>
public class UserPatronage : UserEntityBase<UserPatronage>
{
    public Int64 FollowerCount { get; set; } = 0;

    public Int64 FollowingCount { get; set; } = 0;
    
    public Int64 StyleCount { get; set; } = 0;
    
    public Int64 FavoriteCount { get; set; } = 0;
}


/// <summary>
/// UserRecognition
/// </summary>

public class UserRecognition : UserEntityBase<UserRecognition>
{   
    public Int64 FamousValue { get; set; } = 0;
}
