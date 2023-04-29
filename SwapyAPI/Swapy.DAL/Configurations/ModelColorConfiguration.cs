using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ModelColorConfiguration : IEntityTypeConfiguration<ModelColor>
    {
        public void Configure(EntityTypeBuilder<ModelColor> builder)
        {
            builder.ToTable("ModelsColors");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(m => m.Model)
                   .WithMany(m => m.ModelsColors)
                   .HasForeignKey(m => m.ModelId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(m => m.Color)
                   .WithMany(c => c.ModelsColors)
                   .HasForeignKey(m => m.ColorId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(m => m.ElectronicAttributes)
                   .WithOne(e => e.ModelColor)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
