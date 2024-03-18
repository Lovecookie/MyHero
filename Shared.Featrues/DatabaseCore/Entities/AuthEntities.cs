

using System.Data.SqlTypes;

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
public class UserAuthJwt : UserEntityBase<UserAuthJwt>, IAggregateRoot
{
    [MaxLength(512)]
    public string AccessToken { get; set; } = default!;

    [MaxLength(512)]
    public string RefreshToken { get; set; } = default!;
}

