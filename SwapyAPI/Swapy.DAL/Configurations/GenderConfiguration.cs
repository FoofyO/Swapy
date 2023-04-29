using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    { 
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Genders");
            builder.HasKey(c => c.Id);
             
            builder.Property(c => c.Id)
                .isRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Type)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();  

            builder.HasMany(c => c.ClothesBrandView)
                   .WithOne(p => p.Gender) 
                   .HasForeignKey(p => p.GenderId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired();  
        }
    }
}
