using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ModelColorConfiguration : IEntityTypeConfiguration<ModelColor>
    {
        public void Configure(EntityTypeBuilder<ModelColor> builder)
        {
            builder.ToTable("ModelsColors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.Model)
                .WithMany(x => x.ModelsColors)
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Color)
                .WithMany(x => x.ModelsColors)
                .HasForeignKey(x => x.ColorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.ElectronicAttributes)
                .WithOne(x => x.ModelColor)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
