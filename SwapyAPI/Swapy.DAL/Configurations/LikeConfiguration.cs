using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.DAL.Entities;
namespace Swapy.DAL.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasDefaultValueSql("NEWID()");

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
