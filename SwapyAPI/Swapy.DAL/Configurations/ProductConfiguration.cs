using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Title)
                   .HasColumnType("NVARCHAR(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasColumnType("NVARCHAR(500)")
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(p => p.Price)
                   .HasColumnType("MONEY(10,2)")
                   .HasDefaultValue(0)
                   .HasPrecision(10, 2)
                   .HasDefaultValueSql("0")
                   .IsRequired();

            builder.Property(p => p.Reviews)
                   .HasColumnType("INT")
                   .HasDefaultValue(0)
                   .HasDefaultValueSql("0")
                   .IsRequired();

            builder.Property(p => p.DateTime)
                   .HasColumnType("DATETIME")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.HasOne(p => p.City)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CityId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(p => p.Currency)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CurrencyId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Products)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(p => p.Images)
                   .WithOne(p => p.Product)
                   .HasForeignKey(p => p.ProductId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired();

            builder.HasOne(p => p.Subcategory)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.SubcategoryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(p => p.Category)
                   .WithMany(p => p.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }
}
