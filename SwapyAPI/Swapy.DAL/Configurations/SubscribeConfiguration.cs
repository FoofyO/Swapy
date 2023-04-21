using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class SubscribeConfiguration : IEntityTypeConfiguration<Subscribe>
    {
        public void Configure(EntityTypeBuilder<Subscribe> builder)
        {
            builder.ToTable("Subscribers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(s => s.Seller)
                   .WithMany(u => u.SubscribesAsSeller)
                   .HasForeignKey(s => s.SellerId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(s => s.Subscriber)
                   .WithMany(u => u.SubscribesAsSubscriber)
                   .HasForeignKey(s => s.SubscriberId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
