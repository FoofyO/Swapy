using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ElectronicAttributeConfiguration : IEntityTypeConfiguration<ElectronicAttribute>
    {
        public void Configure(EntityTypeBuilder<ElectronicAttribute> builder)
        {
            builder.ToTable("ElectronicAttributes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.MemoryModel)
                .WithMany(x => x.ElectronicAttributes)
                .HasForeignKey(x => x.MemoryModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.ModelColor)
                .WithMany(x => x.ElectronicAttributes)
                .HasForeignKey(x => x.ModelColorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.Product)
               .WithOne(x => x.ElectronicAttribute)
               .HasForeignKey<Product>(x => x.ElectronicAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
