
using Shared.Features.Constants;

namespace Shared.Features.DatabaseCore;

[DbSchemaAttribute("accountdb", "auth")]
public class AuthDBContext : DBContextAbstract<AuthDBContext>
{
	public DbSet<UserAuthJwt> UserAuthJwts { get; set; }

	public AuthDBContext(DbContextOptions<AuthDBContext> options, ILogger<DBContextAbstract<AuthDBContext>> logger, IMediator mediator)
		: base(options, logger, mediator)
	{
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(SchemaName());

		_InitUserAuthJwt(modelBuilder);
	}

	private void _InitUserAuthJwt(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserAuthJwt>(entity =>
		{
			entity.HasKey(e => e.UserUID);

			entity.Property(e => e.UserUID)
				.IsRequired();

			entity.Property(e => e.AccessToken)
				.IsRequired()
				.HasMaxLength(ConstantLength.JwtToken);

			entity.Property(e => e.RefreshToken)
				.IsRequired()
				.HasMaxLength(ConstantLength.JwtToken);			
		});
	}
}