using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class RealEstateAttributeConfiguration : IEntityTypeConfiguration<RealEstateAttribute>
    {
        public void Configure(EntityTypeBuilder<RealEstateAttribute> builder)
        {
            builder.ToTable("RealEstatesAttributes");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.Area).IsRequired();

            builder.Property(r => r.Rooms).IsRequired(); 
             
            builder.Property(r => r.IsRent)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasOne(r => r.RealEstateType)
                   .WithMany(s => s.RealEstateAttributes)
                   .HasForeignKey(r => r.RealEstateTypeId)
                   .IsRequired(); ;

            builder.HasOne(r => r.Product)
                   .WithOne(p => p.RealEstateAttribute)
                   .HasForeignKey<Product>(p => p.RealEstateAttributeId)
                   .IsRequired();
        }
    }
}
