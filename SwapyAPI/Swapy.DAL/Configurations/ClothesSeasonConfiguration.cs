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

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired
                .HasDefaultValueSql("NEWID()");
             
            builder.Property(i => i.Size)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();

            builder.HasOne(i => i.ClothesAttribute)
                   .WithMany(p => p.ClothesSeason)
                   .HasForeignKey(i => i.ClothesSeasonId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    } 
}
