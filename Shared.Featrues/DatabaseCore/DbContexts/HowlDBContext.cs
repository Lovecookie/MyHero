
using Shared.Features.Constants;

namespace Shared.Features.DatabaseCore;

[SharedDbSchema("Contents", "howl")]
public class HowlDBContext : DBContextAbstract<HowlDBContext>
{
	public DbSet<UserHowl> UserHowls { get; set; }

	public HowlDBContext(DbContextOptions<HowlDBContext> options, ILogger<DBContextAbstract<HowlDBContext>> logger, IMediator mediator, IConfiguration configuration)
		: base(options, logger, mediator, configuration)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(SchemaName());

		_InitHowlMessage(modelBuilder);
	}

	private void _InitHowlMessage(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserHowl>(entity =>
		{
			entity.HasKey(e => new { e.UniqueKey, e.UserUID });

			entity.Property(e => e.UniqueKey)
			.ValueGeneratedOnAdd();

			entity.Property(e => e.Message)
			.IsRequired()
			.HasMaxLength(ConstantLength.HowlMessage);

			entity.Property(e => e.CreateAt)
			.HasDefaultValueSql("NOW()")
			.IsRequired();
		});
	}

}