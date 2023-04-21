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
    public class ItemAttributesConfiguration : IEntityTypeConfiguration<ItemAttributes>
    {
        public void Configure(EntityTypeBuilder<ItemAttributes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.ItemType)
                .WithMany(x => x.ItemAttributes)
                .OnDelete(DeleteBehavior.Cascade);

            ///builder
            ///   .HasOne(x => x.Product)
            ///   .WithOne(x => x.ItemAttribute)
            ///   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}