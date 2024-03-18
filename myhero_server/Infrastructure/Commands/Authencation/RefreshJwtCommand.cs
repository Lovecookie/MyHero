

using myhero_dotnet.Account;
using System.Security.Principal;

namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class RefreshJwtCommand(ClaimsPrincipal principal) : IRequest<TOptional<string>>
{
	public ClaimsPrincipal Principal { get; init; } = principal;
}

/// <summary>
/// 
/// </summary>
public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, TOptional<string>>
{
	private readonly IUserBasicRepository _userBasicRepository;
	private readonly IUserAuthJwtRepository _userAuthJwtRepository;
	private readonly JwtFields _jwtFields;	

	public RefreshJwtCommandHandler(IUserBasicRepository userBasicRepository, IUserAuthJwtRepository userAuthJwtRepository, IOptions<JwtFields> jwtFields)
	{	
		_userBasicRepository = userBasicRepository;
		_userAuthJwtRepository = userAuthJwtRepository;
		_jwtFields = jwtFields.Value;
	}

	public async Task<TOptional<string>> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
	{
		var tokenType = request.Principal.FindFirstValue(CustomClaimType.TokenType);
		if (tokenType == null || tokenType != "refresh")
		{
			return TOptional.Error<string>("Unauthorized");
		}

		var userUIDClaim = request.Principal.FindFirstValue(CustomClaimType.Uxt);
		if (userUIDClaim == null)
		{
			return TOptional.Error<string>("Unauthorized");
		}

		var userUID = await AesEncryption.DecryptAsInt64(userUIDClaim);
		if( userUID == 0)
		{
			return TOptional.Error<string>("Unauthorized");
		}

		var userBasicOpt = await _userBasicRepository.Find(userUID);
		if (!userBasicOpt.HasValue)
		{
			return TOptional.Error<string>("User not found");
		}

		var userBasic = userBasicOpt.Value!;
		
		var accessToken = await TokenHelper.GenerateAccessJwt(userBasic.UserUID, _jwtFields);		

		var bResult = await _userAuthJwtRepository.UpdateAccessToken(userBasic.UserUID, accessToken);
		if (!bResult)
		{
			return TOptional.Error<string>("Error updating refresh token");
		}

		await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);		

		return TOptional.Success(accessToken);
	}
}