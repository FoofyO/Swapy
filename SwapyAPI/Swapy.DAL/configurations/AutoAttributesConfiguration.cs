using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AutoAttributesConfiguration : IEntityTypeConfiguration<AutoAttributes>
    {
        public void Configure(EntityTypeBuilder<AutoAttributes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Miliage)
                .IsRequired();

            builder.Property(x => x.EngineCapacity)
                .IsRequired();

            builder.Property(x => x.ReleaseYear)
                .IsRequired()
                .HasColumnType("DATE");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.FuelType)
                .WithMany(x => x.AutoAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.AutoColor)
                .WithMany(x => x.AutoAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.TransmissionType)
                .WithMany(x => x.AutoAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.AutoBrandType)
               .WithMany(x => x.AutoAttributes)
               .OnDelete(DeleteBehavior.Cascade);

            ///builder
            ///   .HasOne(x => x.Product)
            ///   .WithOne(x => x.AutoAttribute)
            ///   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
