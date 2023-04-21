using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(c => c.Seller)
                   .WithMany(u => u.ChatsAsSeller)
                   .HasForeignKey(c => c.SellerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(c => c.Buyer)
                   .WithMany(u => u.ChatsAsBuyer)
                   .HasForeignKey(c => c.BuyerId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
