using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.ToTable("ItemTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.ItemAttributes)
                .WithOne(x => x.ItemType)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(x => x.Subcategory)
                .WithOne(x => x.ItemType)
                .HasForeignKey<Subcategory>(x => x.ItemTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
