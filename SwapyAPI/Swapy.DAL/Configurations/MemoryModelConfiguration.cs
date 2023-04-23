using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class MemoryModelConfiguration : IEntityTypeConfiguration<MemoryModel>
    {
        public void Configure(EntityTypeBuilder<MemoryModel> builder)
        {
            builder.ToTable("MemoriesModels");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .HasOne(x => x.Memory)
                .WithMany(x => x.MemoriesModels)
                .HasForeignKey(x => x.MemoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Model)
                .WithMany(x => x.MemoriesModels)
                .HasForeignKey(x => x.ModelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.ElectronicAttributes)
                .WithOne(x => x.MemoryModel)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
