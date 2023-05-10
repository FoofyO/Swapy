using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class SubcategoryBranchConfiguration : IEntityTypeConfiguration<SubcategoryBranch>
    {
        public void Configure(EntityTypeBuilder<SubcategoryBranch> builder)
        {
            builder.ToTable("SubcategoryBranches");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).IsRequired();

            builder.HasOne(b => b.Parent)
                   .WithOne(s => s.ParentSubcategory)
                   .HasForeignKey<Subcategory>(s => s.ParentSubcategoryId)
                   .IsRequired(false);

            builder.HasOne(b => b.Child)
                   .WithMany(s => s.ChildSubcategories)
                   .HasForeignKey(b => b.ChildId)
                   .IsRequired();
        }
    }
}
