using Swapy.Common.Entities;
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

            builder.Property(s => s.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

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
                  .IsRequired(false);

            builder.HasMany(s => s.AnimalBreeds)
                   .WithOne(a => a.AnimalType)
                   .HasForeignKey(a => a.AnimalTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(s => s.ClothesViews)
                   .WithOne(c => c.ClothesType)
                   .HasForeignKey(c => c.ClothesTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(s => s.ItemAttributes)
                   .WithOne(i => i.ItemType)
                   .HasForeignKey(i => i.ItemTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(s => s.RealEstateAttributes)
                   .WithOne(r => r.RealEstateType)
                   .HasForeignKey(r => r.RealEstateTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            builder.HasMany(s => s.ElectronicBrandsTypes)
                   .WithOne(e => e.ElectronicType)
                   .HasForeignKey(e => e.ElectronicTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
