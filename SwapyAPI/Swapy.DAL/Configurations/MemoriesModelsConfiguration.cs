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
    public class MemoriesModelsConfiguration : IEntityTypeConfiguration<MemoriesModels>
    {
        public void Configure(EntityTypeBuilder<MemoriesModels> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.Memory)
                .WithMany(x => x.MemoriesModels)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Model)
                .WithMany(x => x.MemoriesModels)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.ElectronicAttributes)
                .WithOne(x => x.MemoryModel)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
