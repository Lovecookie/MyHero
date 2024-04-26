

using System.Data.SqlTypes;

namespace Shared.Features.DatabaseCore;


/// <summary>
///  ChurchGroup
/// </summary>
public class ChurchGroup : KeyEntityBase, IAggregateRoot
{
   public string GroupName { get; set; } = "";
}

