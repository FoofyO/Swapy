﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Configurations
{
    public class AutoColorsConfiguration : IEntityTypeConfiguration<AutoColors>
    {
        public void Configure(EntityTypeBuilder<AutoColors> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.AutoAttributes)
                .WithOne(x => x.AutoColor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
