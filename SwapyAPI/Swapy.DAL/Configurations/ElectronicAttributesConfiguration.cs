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
    public class ElectronicAttributesConfiguration : IEntityTypeConfiguration<ElectronicAttributes>
    {
        public void Configure(EntityTypeBuilder<ElectronicAttributes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.MemoryModel)
                .WithMany(x => x.ElectronicAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.ModelColor)
                .WithMany(x => x.ElectronicAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            ///builder
            ///   .HasOne(x => x.Product)
            ///   .WithOne(x => x.ElectronicAttribute)
            ///   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
