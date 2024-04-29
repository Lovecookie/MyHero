namespace Shared.Features.DatabaseCore;

public abstract class UniqueKeyEntityBase
{
    public long UniqueKey { get; set; }
}

public abstract class KeyEntityBase
{
    public long UniqueKey { get; set; }

    public DateTime DateCreated { get; init; }

    public DateTime DateModified { get; set; }
}