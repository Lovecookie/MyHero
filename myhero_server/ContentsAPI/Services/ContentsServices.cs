
namespace myhero_dotnet.ContentsAPI;

public class ContentsServices(
	IMediator mediator,
	ILogger<ContentsServices> logger,
	IMapper mapper
	)
{
	public IMediator Mediator { get; set; } = mediator;
	public ILogger<ContentsServices> Logger { get; init; } = logger;
	public IMapper Mapper { get; init; } = mapper;
}

