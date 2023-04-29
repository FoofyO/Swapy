using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class MemoryConfiguration : IEntityTypeConfiguration<Memory>
    {
        public void Configure(EntityTypeBuilder<Memory> builder)
        {
            builder.ToTable("Memories");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(m => m.MemoriesModels)
                   .WithOne(m => m.Memory)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}

