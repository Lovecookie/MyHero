using shipcret_server_dotnet.DatabaseCore.Repositories;


namespace shipcret_server_dotnet.DatabaseCore.Entities;



/// <summary>
///  UserBasic
/// </summary>
public class UserBasic : UserEntityBase, IAggregateRoot
{ 
    public string UserId { get; set; } = ""; 
    
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    public string PictureUrl { get; set; } = "";
}


/// <summary>
/// UserPatronage
/// </summary>
public class UserPatronage : UserEntityBase, IAggregateRoot
{   
    public Int64 FollowerCount { get; set; }
    
    public Int64 FollowingCount { get; set; }
    
    public Int64 StyleCount { get; set; }
    
    public Int64 FavoriteCount { get; set; }
}


/// <summary>
/// UserRecognition
/// </summary>
public class UserRecognition : UserEntityBase, IAggregateRoot
{
    [Required]
    public Int64 FamousValue { get; set; }
}
