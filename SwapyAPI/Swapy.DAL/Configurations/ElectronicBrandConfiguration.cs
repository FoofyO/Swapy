using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ElectronicBrandConfiguration : IEntityTypeConfiguration<ElectronicBrand>
    {
        public void Configure(EntityTypeBuilder<ElectronicBrand> builder)
        {
            builder.ToTable("ElectronicBrands");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.ElectronicBrandsTypes)
                .WithOne(x => x.ElectronicBrand)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

