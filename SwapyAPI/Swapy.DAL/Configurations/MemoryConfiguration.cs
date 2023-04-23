using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class MemoryConfiguration : IEntityTypeConfiguration<Memory>
    {
        public void Configure(EntityTypeBuilder<Memory> builder)
        {
            builder.ToTable("Memories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.MemoriesModels)
                .WithOne(x => x.Memory)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

