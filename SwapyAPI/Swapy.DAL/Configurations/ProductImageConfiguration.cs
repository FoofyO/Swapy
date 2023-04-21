using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id).HasDefaultValueSql("NEWID()");

            builder.Property(i => i.Image)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired(false);

            builder.HasOne(i => i.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(i => i.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
