using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;
using System.Globalization;

namespace Swapy.DAL.Configurations
{
    public class ShopAttributeConfiguration : IEntityTypeConfiguration<ShopAttribute>
    {
        public void Configure(EntityTypeBuilder<ShopAttribute> builder)
        {
            builder.ToTable("ShopAttributes");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.ShopName)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();

            builder.Property(s => s.Description)
                   .HasColumnType("NVARCHAR(1024)")
                   .HasMaxLength(1024)
                   .IsRequired(false);

            builder.Property(s => s.Location)
                   .HasColumnType("NVARCHAR(256)")
                   .HasMaxLength(256)
                   .IsRequired(false);

            builder.Property(s => s.Banner)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired(false);

            builder.Property(s => s.WorkDays)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired(false);

            builder.Property(s => s.StartWorkTime)
                   .HasColumnType("TIME")
                   .IsRequired(false);

            builder.Property(s => s.EndWorkTime)
                   .HasColumnType("TIME")
                   .IsRequired(false);

            builder.HasOne(s => s.User)
                   .WithOne(u => u.ShopAttribute)
                   .HasForeignKey<User>(u => u.ShopAttributeId)
                   .IsRequired();
        }
    }
}
