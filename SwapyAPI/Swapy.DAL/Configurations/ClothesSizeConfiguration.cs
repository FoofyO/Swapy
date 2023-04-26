using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesSizeConfiguration : IEntityTypeConfiguration<ClothesSize>
    {
        public void Configure(EntityTypeBuilder<ClothesSize> builder)
        {
            builder.ToTable("ClothesSizes");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsShoe)
                .IsRequired()
                .HasColumnType("BIT");

            builder.Property(x => x.IsChild)
                .IsRequired() 
                .HasColumnType("BIT");
 
            builder.Property(i => i.Size)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(32)
                   .IsRequired(false);

            builder.HasOne(i => i.Product)  
                   .WithMany(p => p.ClothesSize)
                   .HasForeignKey(i => i.ClothesSizeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();   
        }
    }
}
