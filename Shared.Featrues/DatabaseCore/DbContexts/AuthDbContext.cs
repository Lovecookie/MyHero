
using Shared.Features.Constants;
using Shared.Features.Extensions;

namespace Shared.Features.DatabaseCore;

[DbSchemaAttribute("accountdb", "auth")]
public class AuthDbContext : DbContextAbstract<AuthDbContext>
{
	public DbSet<UserAuthJwt> UserAuthJwts { get; set; }

	public AuthDbContext(DbContextOptions<AuthDbContext> options, IMediator mediator)
		: base(options, mediator)
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