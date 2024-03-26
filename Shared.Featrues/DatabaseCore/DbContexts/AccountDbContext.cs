
using Shared.Features.Constants;

namespace Shared.Features.DatabaseCore;

[DbSchema("accountdb", "account")]
public class AccountDBContext : DBContextAbstract<AccountDBContext>
{
	public DbSet<UserBasic> UserBasics { get; set; }
	public DbSet<UserPatronage> UserPatronages { get; set; }
	public DbSet<UserRecognition> UserRecognitions { get; set; }

	public AccountDBContext(DbContextOptions<AccountDBContext> options, ILogger<DBContextAbstract<AccountDBContext>> logger, IMediator mediator)
		: base(options, logger, mediator)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(SchemaName());

		_InitUserBasic(modelBuilder);
		_InitUserPatronage(modelBuilder);
		_InitUserRecognition(modelBuilder);

		//modelBuilder.UseIntegrationEventLogs();
	}

	private void _InitUserBasic(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserBasic>(entity =>
		{
			entity.HasKey(e => e.UserUID);

			entity.HasIndex(e => e.Email)
			.HasDatabaseName("UIX_UserBasic_Email")
			.IsUnique();

			entity.HasIndex(e => e.UserID)
			.HasDatabaseName("UIX_UserBasic_UserId")
			.IsUnique();

			entity.Property(e => e.EncryptedUID)
			.IsRequired()
			.HasMaxLength(ConstantLength.EncryptedUID);

			entity.Property(e => e.UserUID)
			.ValueGeneratedOnAdd();

			entity.Property(e => e.UserID)
			.IsRequired()
			.HasMaxLength(ConstantLength.UserId);

			entity.Property(e => e.Email)
			.IsRequired()
			.HasMaxLength(ConstantLength.EMail);

			entity.Property(e => e.Password)
			.IsRequired()
			.HasMaxLength(ConstantLength.Password);

			entity.Property(e => e.PictureUrl)
			.IsRequired()
			.HasMaxLength(ConstantLength.PictureUrl);

			entity.Property(e => e.DateCreated)
			.HasDefaultValueSql("NOW()")
			.IsRequired();

			entity.Property(e => e.DateModified)
			.HasDefaultValueSql("NOW()")
			.IsRequired();
		});
	}

	private void _InitUserPatronage(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserPatronage>(entity =>
		{
			entity.HasKey(e => e.UserUID);

			entity.Property(e => e.UserUID)
				.ValueGeneratedOnAdd();

			entity.Property(e => e.FollowerCount)
				.IsRequired();

			entity.Property(e => e.FollowingCount)
				.IsRequired();

			entity.Property(e => e.StyleCount)
				.IsRequired();

			entity.Property(e => e.FavoriteCount)
				.IsRequired();

			entity.Property(e => e.DateCreated)
			.HasDefaultValueSql("NOW()")
			.IsRequired();

			entity.Property(e => e.DateModified)
			.HasDefaultValueSql("NOW()")
			.IsRequired();
		});
	}

	private void _InitUserRecognition(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserRecognition>(entity =>
		{
			entity.HasKey(e => e.UserUID);

			entity.Property(e => e.FamousValue)
				.IsRequired();

			entity.Property(e => e.DateCreated)
			.HasDefaultValueSql("NOW()")
			.IsRequired();

			entity.Property(e => e.DateModified)
			.HasDefaultValueSql("NOW()")
			.IsRequired();
		});
	}
}