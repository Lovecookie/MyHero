﻿
using Serilog.Core;
using myhero_dotnet.Account.Extensions;
using myhero_dotnet.DatabaseCore.Entities;
using myhero_dotnet.DatabaseCore.Repositories;
using myhero_dotnet.Infrastructure.Constants;

namespace myhero_dotnet.DatabaseCore.DbContexts;

[DbSchemaAttribute("account", "accountdb")]
public class AccountDbContext : DbContextAbstract<AccountDbContext>
{
	public DbSet<UserBasic> UserBasics { get; set; }
	public DbSet<UserPatronage> UserPatronages { get; set; }
	public DbSet<UserRecognition> UserRecognitions { get; set; }


	public AccountDbContext(DbContextOptions<AccountDbContext> options)
		: base(options)
	{
	}

	public AccountDbContext(DbContextOptions<AccountDbContext> options, IMediator mediator)
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

		_InitUserBasic(modelBuilder);
		_InitUserPatronage(modelBuilder);
		_InitUserRecognition(modelBuilder);

		//modelBuilder.UseIntegrationEventLogs();
	}

	private void _InitUserBasic(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserBasic>(entity =>
		{
			// entity.ToTable(UserBasic.TableName());

			entity.HasKey(e => e.UserUid);

			entity.Property(e => e.UserUid)
				.ValueGeneratedOnAdd();

			entity.Property(e => e.UserId)
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
				.IsRequired();

			entity.Property(e => e.DateModified)
				.IsRequired();
		});
	}

	private void _InitUserPatronage(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserPatronage>(entity =>
		{
			entity.HasKey(e => e.UserUid);

			entity.Property(e => e.UserUid)
				.ValueGeneratedOnAdd();

			entity.Property(e => e.FollowerCount)
				.IsRequired();

			entity.Property(e => e.FollowingCount)
				.IsRequired();

			entity.Property(e => e.StyleCount)
				.IsRequired();

			entity.Property(e => e.FavoriteCount)
				.IsRequired();
		});
	}

	private void _InitUserRecognition(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserRecognition>(entity =>
		{
			entity.HasKey(e => e.UserUid);

			entity.Property(e => e.FamousValue)
				.IsRequired();
		});
	}
}