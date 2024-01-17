namespace shipcret_server_dotnet.DatabaseCore.Entities;

public abstract class UserEntityBase
{
    [Key]
    public long UserUid { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}