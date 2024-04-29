
using Shared.Features.Constants;

namespace Shared.Features.DatabaseCore;

[DbSchema("contentsdb", "contents")]
public class HowlDBContext : DBContextAbstract<HowlDBContext>
{
	public DbSet<HowlMessage> HowlMessages { get; set; }

	public HowlDBContext(DbContextOptions<HowlDBContext> options, ILogger<DBContextAbstract<HowlDBContext>> logger, IMediator mediator)
		: base(options, logger, mediator)
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
		modelBuilder.Entity<HowlMessage>(entity =>
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