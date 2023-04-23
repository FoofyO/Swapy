﻿using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class SubcategoryConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable("Subcategories");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(50)")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(s => s.PrevSubcategory)
                   .WithMany(s => s.Subcategories)
                   .HasForeignKey(s => s.Id)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);

            builder.HasMany(s => s.Subcategories)
                   .WithOne(s => s)
                   .HasForeignKey(s => s.Id)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(s => s.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(s => s.Products)
                  .WithOne(c => c.Subcategory)
                  .HasForeignKey(s => s.SubcategoryId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired();
<<<<<<< HEAD

            builder.HasOne(s => s.AutoType)
                .WithOne(c => c.Subcategory)
                .HasForeignKey<AutoType>(s => s.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(s => s.ItemType)
                .WithOne(c => c.Subcategory)
                .HasForeignKey<ItemType>(s => s.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
=======
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        }
    }
}
