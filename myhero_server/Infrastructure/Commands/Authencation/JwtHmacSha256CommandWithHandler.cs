
namespace myhero_dotnet.Infrastructure;


public class JwtHmacSha256CommandWithHandler : IRequestHandler<JwtHmacSha256Command, TOptional<string>>
{
    private readonly JwtFields _jwtFields;

    public JwtHmacSha256CommandWithHandler(IOptions<JwtFields> jwtFields)
    {
        _jwtFields = jwtFields.Value;
    }

    public async Task<TOptional<string>> Handle(JwtHmacSha256Command request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, request.Request.Email)
        };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtFields.Secret));

        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtFields.Issuer,
            audience: _jwtFields.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return TOptional.To(tokenString);
    }
}