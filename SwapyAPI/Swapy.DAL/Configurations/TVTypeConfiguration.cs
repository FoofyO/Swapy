﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class TVTypeConfiguration : IEntityTypeConfiguration<TVType>
    {
        public void Configure(EntityTypeBuilder<TVType> builder)
        {
            builder.ToTable("TVTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.TVAttributes)
                .WithOne(x => x.TVType)
                .HasForeignKey(x => x.TVTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
