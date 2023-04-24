using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class RealEstateAttributeConfiguration : IEntityTypeConfiguration<RealEstateAttribute>
    {
        public void Configure(EntityTypeBuilder<RealEstateAttribute> builder)
        {
            builder.ToTable("RealEstatesAttributes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Area)
                .IsRequired();

            builder.Property(x => x.Rooms)
                .IsRequired(); 
             
            builder.Property(x => x.IsRent)
                .IsRequired()
                .HasColumnType("BIT");
              
            builder
               .HasOne(x => x.RealEstateType)
               .WithMany(x => x.RealEstateAttributes)
               .HasForeignKey(p => p.RealEstateTypeId)
               .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.Product)
               .WithOne(x => x.RealEstateAttribute)
               .HasForeignKey<Product>(x => x.RealEstateAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
