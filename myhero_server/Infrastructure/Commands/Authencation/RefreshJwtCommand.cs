

using myhero_dotnet.Account;

namespace myhero_dotnet.Infrastructure;

/// <summary>
/// 
/// </summary>
/// <param name="Request"></param>
public class RefreshJwtCommand(Int64 userUID) : IRequest<TOptional<string>>
{
	public Int64 UserUID { get; init; } = userUID;
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
		var userBasicOpt = await _userBasicRepository.Find(request.UserUID);
		if (!userBasicOpt.HasValue)
		{
			return TOptional.Error<string>("User not found");
		}

		var userBasic = userBasicOpt.Value!;
		
		var accessToken = TokenHelper.GenerateAccessJwt(userBasic.UserUID, userBasic.Email, _jwtFields);		
		var encryptAccess = await AesEncryption.EncryptAsync(accessToken);

		var bResult = await _userAuthJwtRepository.UpdateAccessToken(userBasic.UserUID, encryptAccess);
		if (!bResult)
		{
			return TOptional.Error<string>("Error updating refresh token");
		}

		await _userAuthJwtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);		

		return TOptional.To(accessToken);
	}
}