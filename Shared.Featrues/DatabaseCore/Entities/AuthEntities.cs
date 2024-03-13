

using System.Data.SqlTypes;

namespace Shared.Features.DatabaseCore;


/// <summary>
///  UserBasic
/// </summary>
public class UserAuthJwt : UserEntityBase<UserAuthJwt>, IAggregateRoot
{
    [MaxLength(512)]
    public byte[] AccessToken { get; set; } = new byte[512];

    [MaxLength(512)]
    public byte[] RefreshToken { get; set; } = new byte[512];
}

