using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AutoColorConfiguration : IEntityTypeConfiguration<AutoColor>
    {
        public void Configure(EntityTypeBuilder<AutoColor> builder)
        {
            builder.ToTable("AutoColors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.AutoAttributes)
                .WithOne(x => x.AutoColor)
                .HasForeignKey(x => x.AutoColorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
