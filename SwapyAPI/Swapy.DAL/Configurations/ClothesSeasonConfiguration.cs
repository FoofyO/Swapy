using Swapy.Common.Entities;
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

            builder.Property(c => c.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(c => c.ClothesAttributes)
                   .WithOne(c => c.ClothesSeason)
                   .HasForeignKey(i => i.ClothesSeasonId)
                   .IsRequired(false);
        }
    } 
}
