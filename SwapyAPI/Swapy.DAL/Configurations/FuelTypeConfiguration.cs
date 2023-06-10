using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class FuelTypeConfiguration : IEntityTypeConfiguration<FuelType>
    {
        public void Configure(EntityTypeBuilder<FuelType> builder)
        {
            builder.ToTable("FuelTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(x => x.AutoAttributes)
                   .WithOne(x => x.FuelType)
                   .HasForeignKey(x => x.FuelTypeId)
                   .IsRequired(false);
        }
    }
}
