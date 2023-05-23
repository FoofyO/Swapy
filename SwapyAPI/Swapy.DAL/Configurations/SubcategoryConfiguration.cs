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

            builder.Property(s => s.Id).IsRequired();

            builder.Property(s => s.Name)
                   .HasColumnType("NVARCHAR(50)")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(s => s.ParentSubcategory)
                   .WithOne(s => s.Parent)
                   .HasForeignKey<SubcategoryBranch>(s => s.ParentId)
                   .IsRequired();

            builder.HasMany(s => s.ChildSubcategories)
                   .WithOne(s => s.Child)
                   .HasForeignKey(s => s.ChildId)
                   .IsRequired(false);

            builder.HasOne(s => s.Category)
                   .WithMany(c => c.Subcategories)
                   .HasForeignKey(s => s.CategoryId)
                   .IsRequired();

            builder.HasMany(s => s.Products)
                  .WithOne(c => c.Subcategory)
                  .HasForeignKey(s => s.SubcategoryId)
                  .IsRequired(false);

            builder.HasMany(s => s.AnimalBreeds)
                   .WithOne(a => a.AnimalType)
                   .HasForeignKey(a => a.AnimalTypeId)
                   .IsRequired(false);

            builder.HasMany(s => s.ClothesViews)
                   .WithOne(c => c.ClothesType)
                   .HasForeignKey(c => c.ClothesTypeId)
                   .IsRequired(false);

            builder.HasMany(s => s.ItemAttributes)
                   .WithOne(i => i.ItemType)
                   .HasForeignKey(i => i.ItemTypeId)
                   .IsRequired(false);

            builder.HasMany(s => s.AutoModels)
                   .WithOne(am => am.AutoType)
                   .HasForeignKey(r => r.AutoTypeId)
                   .IsRequired(false);

            builder.HasMany(s => s.RealEstateAttributes)
                   .WithOne(r => r.RealEstateType)
                   .HasForeignKey(r => r.RealEstateTypeId)
                   .IsRequired(false);

            builder.HasMany(s => s.ElectronicBrandsTypes)
                   .WithOne(e => e.ElectronicType)
                   .HasForeignKey(e => e.ElectronicTypeId)
                   .IsRequired(false);
        }
    }
}
