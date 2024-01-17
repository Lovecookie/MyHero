namespace myhero_dotnet.DatabaseCore.Entities;

internal abstract record KeyEntityBase
{
    public long UniqueKey { get; set; }

    public DateTime DateCreated { get; init; }

    public DateTime DateModified { get; set; }
}