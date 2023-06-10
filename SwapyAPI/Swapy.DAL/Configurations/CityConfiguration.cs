using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(c => c.Products)
                   .WithOne(p => p.City)
                   .HasForeignKey(p => p.CityId)
                   .IsRequired(false);
        }
    }
}
