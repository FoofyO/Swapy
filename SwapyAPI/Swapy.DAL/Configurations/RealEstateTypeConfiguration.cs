using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class RealEstateTypeConfiguration : IEntityTypeConfiguration<RealEstateType>
    {
        public void Configure(EntityTypeBuilder<RealEstateType> builder)
        {
            builder.ToTable("RealEstateTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasOne(x => x.Subcategory)
                .WithOne(x => x.RealEstateType)
                .HasForeignKey<Subcategory>(x => x.RealEstateTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.RealEstateAttributes)
                .WithOne(x => x.RealEstateType)
                .OnDelete(DeleteBehavior.SetNull);

        } 
    }
}
