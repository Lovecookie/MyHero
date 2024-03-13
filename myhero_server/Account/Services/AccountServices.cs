﻿
namespace myhero_dotnet.Account;

public class AuthServices(
	IMediator mediator,
	ILogger<AuthServices> logger,
	IMapper mapper
	)
{
	public IMediator Mediator { get; set; } = mediator;
	public ILogger<AuthServices> Logger { get; init; } = logger;
	public IMapper Mapper { get; init; } = mapper;
}

