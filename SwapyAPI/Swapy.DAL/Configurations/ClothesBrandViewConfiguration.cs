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
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Name) 
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired(); 

            builder.HasOne(s => s.ClothesBrand)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(s => s.ClothesBrandId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(); 
             
            builder.HasOne(s => s.ClothesView)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(s => s.ClothesViewId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(); 
             
            builder.HasMany(s => s.ClothesAttribute)
                  .WithOne(c => c.ClothesBrandView) 
                  .HasForeignKey(s => s.ClothesBrandViewId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired();  
        } 
    }
}

 