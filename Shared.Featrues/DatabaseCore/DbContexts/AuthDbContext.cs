
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

	public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
	{
		if (HasMediator)
		{
			await Mediator!.DispatchDomainEventAsync(this);			
		}

		_ = base.SaveChangesAsync(cancellationToken);

		return true;
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
			entity.HasKey(e => e.UserUid);

			entity.Property(e => e.UserUid)
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