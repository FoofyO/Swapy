using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AutoTypeConfiguration : IEntityTypeConfiguration<AutoType>
    {
        public void Configure(EntityTypeBuilder<AutoType> builder)
        {
            builder.ToTable("AutoTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.AutoBrandsTypes)
                .WithOne(x => x.AutoType)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(x => x.Subcategory)
                .WithOne(x => x.AutoType)
                .HasForeignKey<Subcategory>(x => x.AutoTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
