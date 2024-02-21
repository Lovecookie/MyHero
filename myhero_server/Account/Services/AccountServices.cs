
namespace myhero_dotnet.Account;

public class AccountServices(
	IMediator mediator,
	ILogger<AccountServices> logger,
	IMapper mapper
	)
{
	public IMediator Mediator { get; set; } = mediator;
	public ILogger<AccountServices> Logger { get; init; } = logger;
	public IMapper Mapper { get; init; } = mapper;
}

