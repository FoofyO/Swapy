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
    public class ElectronicBrandsTypesConfiguration : IEntityTypeConfiguration<ElectronicBrandsTypes>
    {
        public void Configure(EntityTypeBuilder<ElectronicBrandsTypes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.ElectronicBrand)
                .WithMany(x => x.ElectronicBrandsTypes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.ElectronicType)
                .WithMany(x => x.ElectronicBrandsTypes)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Models)
                .WithOne(x => x.ElectronicBrandType)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
