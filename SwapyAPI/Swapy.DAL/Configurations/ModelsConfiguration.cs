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
    public class ModelsConfiguration : IEntityTypeConfiguration<Models>
    {
        public void Configure(EntityTypeBuilder<Models> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasOne(x => x.ElectronicBrandType)
                .WithMany(x => x.Models)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.MemoriesModels)
                .WithOne(x => x.Model)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.ModelsColors)
                .WithOne(x => x.Model)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
