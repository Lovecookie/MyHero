

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
[DbTableAttribute("user_basic")]
public class UserBasic : UserEntityBase<UserBasic>, IAggregateRoot
{ 
    public string UserId { get; set; } = ""; 
    
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    public string PictureUrl { get; set; } = "";
}


/// <summary>
/// UserPatronage
/// </summary>
[DbTableAttribute("user_patronage")]
public class UserPatronage : UserEntityBase<UserPatronage>, IAggregateRoot
{   
    public Int64 FollowerCount { get; set; }
    
    public Int64 FollowingCount { get; set; }
    
    public Int64 StyleCount { get; set; }
    
    public Int64 FavoriteCount { get; set; }
}


/// <summary>
/// UserRecognition
/// </summary>
[DbTableAttribute("user_recognition")]
public class UserRecognition : UserEntityBase<UserRecognition>, IAggregateRoot
{
    [Required]
    public Int64 FamousValue { get; set; }
}
