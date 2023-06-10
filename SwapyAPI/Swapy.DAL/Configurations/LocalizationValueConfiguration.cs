using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Swapy.Common.Entities;

namespace Swapy.DAL.Configurations
{
    public class LocalizationValueConfiguration : IEntityTypeConfiguration<LocalizationValue>
    {
        public void Configure(EntityTypeBuilder<LocalizationValue> builder)
        {
            builder.ToTable("LocalizationValues");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).IsRequired();

            builder.Property(l => l.Language)
                   .HasColumnType("INT")
                   .IsRequired();

            builder.Property(l => l.Value)
                   .HasColumnType("NVARCHAR(128)")
                   .HasMaxLength(128)
                   .IsRequired();
        }
    }
}
