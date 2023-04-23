using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ItemAttributeConfiguration : IEntityTypeConfiguration<ItemAttribute>
    {
        public void Configure(EntityTypeBuilder<ItemAttribute> builder)
        {
            builder.ToTable("ItemAttributes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.ItemType)
                .WithMany(x => x.ItemAttributes)
                .HasForeignKey(x => x.ItemTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.Product)
               .WithOne(x => x.ItemAttribute)
               .HasForeignKey<Product>(x => x.ItemAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}