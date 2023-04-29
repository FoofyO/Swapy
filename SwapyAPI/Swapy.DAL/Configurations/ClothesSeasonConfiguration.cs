using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesSeasonConfiguration : IEntityTypeConfiguration<ClothesSeason>
    {
        public void Configure(EntityTypeBuilder<ClothesSeason> builder)
        {
            builder.ToTable("ClothesSeasons");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");
             
            builder.Property(c => c.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();

            builder.HasMany(c => c.ClothesAttributes)
                   .WithOne(c => c.ClothesSeason)
                   .HasForeignKey(i => i.ClothesSeasonId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    } 
}
