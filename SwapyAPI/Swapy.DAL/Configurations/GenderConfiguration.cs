using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    { 
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");
            builder.HasKey(g => g.Id);
             
            builder.Property(g => g.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(g => g.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();  

            builder.HasMany(g => g.ClothesViews)
                   .WithOne(c => c.Gender) 
                   .HasForeignKey(c => c.GenderId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);  
        }
    }
}
