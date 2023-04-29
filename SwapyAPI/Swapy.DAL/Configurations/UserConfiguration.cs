using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.FullName)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(u => u.Logo)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired(false);

            builder.Property(u => u.RegistrationDate)
                   .HasColumnType("DATE")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasMany(u => u.Products)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.LikesAsLiker)
                   .WithOne(l => l.Liker)
                   .HasForeignKey(l => l.LikerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.LikesAsSeller)
                   .WithOne(l => l.Seller)
                   .HasForeignKey(l => l.SellerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.ChatsAsBuyer)
                   .WithOne(c => c.Seller)
                   .HasForeignKey(c => c.SellerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.ChatsAsSeller)
                   .WithOne(c => c.Seller)
                   .HasForeignKey(c => c.SellerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.SentMessages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.SubscribesAsSeller)
                   .WithOne(s => s.Seller)
                   .HasForeignKey(s => s.SellerId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(u => u.SubscribesAsSubscriber)
                   .WithOne(s => s.Subscriber)
                   .HasForeignKey(s => s.SubscriberId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
