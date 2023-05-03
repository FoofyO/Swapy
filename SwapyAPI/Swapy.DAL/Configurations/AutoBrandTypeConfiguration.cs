using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class AutoBrandTypeConfiguration : IEntityTypeConfiguration<AutoBrandType>
    {
        public void Configure(EntityTypeBuilder<AutoBrandType> builder)
        {
            builder.ToTable("AutoBrandsTypes");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(a => a.AutoBrand)
                   .WithMany(a => a.AutoBrandsTypes)
                   .HasForeignKey(a => a.AutoBrandId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(a => a.AutoType)
                   .WithMany(s => s.AutoBrandTypes)
                   .HasForeignKey(s => s.AutoTypeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(a => a.AutoAttributes)
                   .WithOne(a => a.AutoBrandType)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
