using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(s => s.Subscriber)
                   .WithMany(u => u.Subscriptions)
                   .HasForeignKey(s => s.SubscriberId)
                   .IsRequired();

            builder.HasOne(s => s.UserSubscription)
                   .WithOne(us => us.Subscription)
                   .HasForeignKey<UserSubscription>(us => us.SubscriptionId)
                   .IsRequired();
        }
    }
}
