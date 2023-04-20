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
    public class AutoBrandsConfiguration : IEntityTypeConfiguration<AutoBrands>
    {
        public void Configure(EntityTypeBuilder<AutoBrands> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.AutoBrandsTypes)
                .WithOne(x => x.AutoBrand)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
