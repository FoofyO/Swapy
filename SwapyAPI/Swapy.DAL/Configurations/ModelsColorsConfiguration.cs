using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Configurations
{
    public class ModelsColorsConfiguration : IEntityTypeConfiguration<ModelsColors>
    {
        public void Configure(EntityTypeBuilder<ModelsColors> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.Model)
                .WithMany(x => x.ModelsColors)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Color)
                .WithMany(x => x.ModelsColors)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.ElectronicAttributes)
                .WithOne(x => x.ModelColor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
