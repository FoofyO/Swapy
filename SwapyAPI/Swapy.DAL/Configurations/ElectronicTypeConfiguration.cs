﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ElectronicTypeConfiguration : IEntityTypeConfiguration<ElectronicType>
    {
        public void Configure(EntityTypeBuilder<ElectronicType> builder)
        {
            builder.ToTable("ElectronicTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.ElectronicBrandsTypes)
                .WithOne(x => x.ElectronicType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}