using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesViewConfiguration : IEntityTypeConfiguration<ClothesView>
    {
        public void Configure(EntityTypeBuilder<ClothesView> builder)
        {
            builder.ToTable("ClothesViews");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .isRequired()
                .HasDefaultValueSql("NEWID()");
             
            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();

            builder.Property(x => x.IsChild)
                .IsRequired()
                .HasColumnType("BIT");  
              
            builder.HasOne(s => s.Gender)
                   .WithMany(s => s.ClothesViews) 
                   .HasForeignKey(s => s.GenderId) 
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(); 
               
            builder.HasOne(s => s.ClothesType)  
                   .WithMany(c => c.ClothesViews) 
                   .HasForeignKey(s => s.ClothesTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();  
           
            builder.HasMany(s => s.ClothesBrandView)
                  .WithOne(c => c.ClothesView)
                  .HasForeignKey(s => s.ClothesViewId)
                  .OnDelete(DeleteBehavior.SetNull) 
                  .IsRequired();  
             
        }
    } 
}     
