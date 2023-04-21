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
    public class ElectronicTypesConfiguration : IEntityTypeConfiguration<ElectronicTypes>
    {
        public void Configure(EntityTypeBuilder<ElectronicTypes> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.ElectronicBrandsTypes)
                .WithOne(x => x.ElectronicType)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}