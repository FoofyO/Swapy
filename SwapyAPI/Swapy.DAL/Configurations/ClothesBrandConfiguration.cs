﻿using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ClothesBrandConfiguration : IEntityTypeConfiguration<ClothesBrand>
    { 
        public void Configure(EntityTypeBuilder<ClothesBrand> builder)
        {
            builder.ToTable("ClothesBrands"); 
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .isRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired(); 

            builder.HasMany(c => c.ClothesBrandView)
                   .WithOne(p => p.ClothesBrand)
                   .HasForeignKey(p => p.ClothesBrandId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(); 
        }
    } 
} 
 