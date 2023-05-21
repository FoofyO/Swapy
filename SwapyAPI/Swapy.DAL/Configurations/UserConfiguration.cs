using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(u => u.UserName).IsRequired(true);

            builder.Property(u => u.FirstName)
                   .HasColumnType("NVARCHAR(64)")
                   .HasMaxLength(64)
                   .IsRequired(false);

            builder.Property(u => u.LastName)
                   .HasColumnType("NVARCHAR(64)")
                   .HasMaxLength(64)
                   .IsRequired(false);

            builder.Property(u => u.Type)
                   .HasColumnType("INT")
                   .IsRequired();

            builder.Property(u => u.RegistrationDate)
                   .HasColumnType("DATE")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(u => u.Logo)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.HasOne(u => u.UserToken)
                   .WithOne(ur => ur.User)
                   .HasForeignKey<UserToken>(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasOne(u => u.ShopAttribute)
                   .WithOne(s => s.User)
                   .HasForeignKey<ShopAttribute>(s => s.UserId)
                   .IsRequired();

            builder.HasMany(u => u.Likes)
                   .WithOne(l => l.Liker)
                   .HasForeignKey(l => l.LikerId)
                   .IsRequired(false);

            builder.HasMany(u => u.Subscriptions)
                   .WithOne(s => s.Subscriber)
                   .HasForeignKey(s => s.SubscriberId)
                   .IsRequired(false);

            builder.HasMany(u => u.ChatsAsBuyer)
                   .WithOne(c => c.Buyer)
                   .HasForeignKey(c => c.BuyerId)
                   .IsRequired(false);

            builder.HasMany(u => u.FavoriteProducts)
                   .WithOne(f => f.User)
                   .HasForeignKey(f => f.UserId)
                   .IsRequired(false);
        }
    }
}
