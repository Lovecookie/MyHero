

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
// [Index(nameof(UserID), IsUnique = true)]
// [Index(nameof(Email), IsUnique = true)]
public class HowlMessage(Int64 userUid, string msg) : UniqueKeyEntityBase, IAggregateRoot
{   
    public Int64 UserUID { get; set; } = userUid;
    
    public string Message { get; set; } = msg;

    public DateTime CreateAt { get; set; } = DateTime.Now;    
}
