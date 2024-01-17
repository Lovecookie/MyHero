
namespace shipcret_server_dotnet.Infrastructure;

//public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
//{
//	public void Configure(EntityTypeBuilder<User> builder)
//	{
//		builder.ToTable("users");
//		builder.HasKey(x => x.Uid);
//		builder.Property(x => x.Uid).ValueGeneratedOnAdd();
//		builder.Property(x => x.UserName).IsRequired();
//		builder.Property(x => x.Email).IsRequired();
//		builder.Property(x => x.Password).IsRequired();
//		builder.Property(x => x.PictureUrl).IsRequired();
//		builder.Property(x => x.PictureName).IsRequired();

//		builder.OwnsOne(o => o.UserName);
//		//builder.Property("_userName").HasColumnName("UserName").IsRequired();
//	}
//}