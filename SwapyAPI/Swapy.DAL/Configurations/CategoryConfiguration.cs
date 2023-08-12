using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.Type)
                   .HasColumnType("INT")
                   .IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(e => e.Subcategories)
                   .WithOne(e => e.Category)
                   .HasForeignKey(e => e.CategoryId)
                   .IsRequired(false);
        }
    }
}
