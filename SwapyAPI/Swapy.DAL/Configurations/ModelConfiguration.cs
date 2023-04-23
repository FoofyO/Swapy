using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Models");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasOne(x => x.ElectronicBrandType)
                .WithMany(x => x.Models)
                .HasForeignKey(x => x.ElectronicBrandTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.MemoriesModels)
                .WithOne(x => x.Model)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(x => x.ModelsColors)
                .WithOne(x => x.Model)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
