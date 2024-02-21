namespace myhero_dotnet.Infrastructure;


public class JwtHmacSha256Command : IRequest<TOptional<string>>
{
    public SharedLoginRequest Request { get; set; } = new();
}