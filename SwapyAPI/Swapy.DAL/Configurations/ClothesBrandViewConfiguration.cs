using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesBrandViewConfiguration : IEntityTypeConfiguration<ClothesBrandView>
    {
        public void Configure(EntityTypeBuilder<ClothesBrandView> builder)
        {
            builder.ToTable("ClothesBrandsViews");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name) 
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired(); 

            builder.HasOne(c => c.ClothesView)
                   .WithMany(c => c.ClothesBrandViews)
                   .HasForeignKey(c => c.ClothesViewId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(c => c.ClothesBrand)
                   .WithMany(c => c.ClothesBrandsViews)
                   .HasForeignKey(c => c.ClothesBrandId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(s => s.ClothesAttributes)
                   .WithOne(c => c.ClothesBrandView) 
                   .HasForeignKey(s => s.ClothesBrandViewId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);  
        } 
    }
}

 