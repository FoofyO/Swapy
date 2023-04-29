using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(l => l.Seller)
                   .WithMany(u => u.LikesAsSeller)
                   .HasForeignKey(l => l.SellerId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(l => l.Liker)
                   .WithMany(u => u.LikesAsLiker)
                   .HasForeignKey(l => l.LikerId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
