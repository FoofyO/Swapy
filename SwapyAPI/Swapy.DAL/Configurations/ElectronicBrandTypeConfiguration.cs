using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ElectronicBrandTypeConfiguration : IEntityTypeConfiguration<ElectronicBrandType>
    {
        public void Configure(EntityTypeBuilder<ElectronicBrandType> builder)
        {
            builder.ToTable("ElectronicBrandsTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.ElectronicBrand)
                .WithMany(x => x.ElectronicBrandsTypes)
                .HasForeignKey(x => x.ElectronicBrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.ElectronicType)
                .WithMany(x => x.ElectronicBrandsTypes)
                .HasForeignKey(x => x.ElectronicTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Models)
                .WithOne(x => x.ElectronicBrandType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
