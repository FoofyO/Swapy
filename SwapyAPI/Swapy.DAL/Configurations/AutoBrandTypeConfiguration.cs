using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AutoBrandTypeConfiguration : IEntityTypeConfiguration<AutoBrandType>
    {
        public void Configure(EntityTypeBuilder<AutoBrandType> builder)
        {
            builder.ToTable("AutoBrandsTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.AutoBrand)
                .WithMany(x => x.AutoBrandsTypes)
                .HasForeignKey(p => p.AutoBrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.AutoType)
                .WithMany(x => x.AutoBrandsTypes)
                .HasForeignKey(p => p.AutoTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.AutoAttributes)
                .WithOne(x => x.AutoBrandType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
