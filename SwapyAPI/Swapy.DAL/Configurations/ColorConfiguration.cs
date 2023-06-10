using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Colors");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(c => c.ModelsColors)
                   .WithOne(m => m.Color)
                   .IsRequired(false);

            builder.HasMany(c => c.AutoAttributes)
                   .WithOne(a => a.AutoColor)
                   .IsRequired(false);
        }
    }
}

