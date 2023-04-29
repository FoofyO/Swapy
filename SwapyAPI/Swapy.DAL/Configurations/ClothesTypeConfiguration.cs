using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesTypeConfiguration : IEntityTypeConfiguration<ClothesType>
    {
        public void Configure(EntityTypeBuilder<ClothesType> builder)
        {
            builder.ToTable("ClothesTypes");
            builder.HasKey(c => c.Id); 

            builder.Property(c => c.Id)
                .isRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();
              
            builder.HasMany(c => c.ClothesBrandView)
                   .WithOne(p => p.ClothesType) 
                   .HasForeignKey(p => p.ClothesTypeId)
                   .OnDelete(DeleteBehavior.SetNull) 
                   .IsRequired(); 
        } 
    }
}  