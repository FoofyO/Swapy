using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class MemoryModelConfiguration : IEntityTypeConfiguration<MemoryModel>
    {
        public void Configure(EntityTypeBuilder<MemoryModel> builder)
        {
            builder.ToTable("MemoriesModels");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.HasOne(m => m.Memory)
                   .WithMany(m => m.MemoriesModels)
                   .HasForeignKey(m => m.MemoryId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(m => m.Model)
                   .WithMany(m => m.MemoriesModels)
                   .HasForeignKey(m => m.ModelId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasMany(m => m.ElectronicAttributes)
                   .WithOne(e => e.MemoryModel)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
