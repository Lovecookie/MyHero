namespace myhero_dotnet.AccountAPI;

public record CreateUserResponse(TokenInfo Token)
{
}

/// <summary>
/// Response for creating a user
/// </summary>
public record SearchUserResponse(string UserID, string PicUrl)
{
}

/// <summary>
/// 
/// </summary>
/// <param name="Token"></param>
public record SignInResponse(string Name, string Email, TokenInfo Token)
{
}

/// <summary>
/// 
/// </summary>
/// <param name="AccessToken"></param>
public record RefreshTokenResposne(string AccessToken)
{
}

public record LogoutResponse
{
}

/// <summary>
/// created token 
/// </summary>
public record AccessTokenResponse(TokenInfo Token)
{
}


/// <summary>
/// 
/// </summary>
/// <param name="FollowersCount"></param>
/// <param name="FollowingCount"></param>
/// <param name="Posts"></param>
/// <param name="FamousValue"></param>
public record MyProfileResponse(string PicURL, Int64 FollowersCount, Int64 FollowingCount, Int64 Posts, Int64 FamousValue )
{
}