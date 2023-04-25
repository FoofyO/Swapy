using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class TVBrandConfiguration : IEntityTypeConfiguration<TVBrand>
    {
        public void Configure(EntityTypeBuilder<TVBrand> builder)
        {
            builder.ToTable("TVBrands");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32); 

            builder
                .HasMany(x => x.TVAttributes)
                .WithOne(x => x.TVBrand)
                .HasForeignKey(x => x.TVBrandId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
