using Swapy.DAL.Entities;
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

            builder.Property(c => c.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(c => c.ModelsColors)
                   .WithOne(m => m.Color)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(c => c.AutoAttributes)
                   .WithOne(a => a.AutoColor)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}

