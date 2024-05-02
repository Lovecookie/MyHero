

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
public class UserHowl : UniqueKeyEntityBase, IAggregateRoot
{   
    public Int64 UserUID { get; set; } = 0;
    
    public string Message { get; set; } = "";

    public DateTime CreateAt { get; set; } = DateTime.Now;
}
