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
    public class AutoBrandsTypesConfiguration : IEntityTypeConfiguration<AutoBrandsTypes>
    {
        public void Configure(EntityTypeBuilder<AutoBrandsTypes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.AutoBrand)
                .WithMany(x => x.AutoBrandsTypes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.AutoType)
                .WithMany(x => x.AutoBrandsTypes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.AutoAttributes)
                .WithOne(x => x.AutoBrandType)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
