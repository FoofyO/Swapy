using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AutoAttributeConfiguration : IEntityTypeConfiguration<AutoAttribute>
    {
        public void Configure(EntityTypeBuilder<AutoAttribute> builder)
        {
            builder.ToTable("AutoAttributes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Miliage).IsRequired();

            builder.Property(x => x.EngineCapacity).IsRequired();

            builder.Property(x => x.ReleaseYear)
                   .IsRequired()
                   .HasColumnType("DATE");

            builder.Property(x => x.IsNew)
                   .IsRequired()
                   .HasColumnType("BIT");

            builder.HasOne(x => x.FuelType)
                   .WithMany(x => x.AutoAttributes)
                   .HasForeignKey(p => p.FuelTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(x => x.AutoColor)
                   .WithMany(x => x.AutoAttributes)
                   .HasForeignKey(p => p.AutoColorId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(x => x.TransmissionType)
                   .WithMany(x => x.AutoAttributes)
                   .HasForeignKey(p => p.TransmissionTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(x => x.AutoBrandType)
                   .WithMany(x => x.AutoAttributes)
                   .HasForeignKey(p => p.AutoBrandTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(x => x.Product)
                   .WithOne(x => x.AutoAttribute)
                   .HasForeignKey<Product>(x => x.AutoAttributeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}
