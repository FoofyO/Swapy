using Swapy.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class ScreenResolutionConfiguration : IEntityTypeConfiguration<ScreenResolution>
    {
        public void Configure(EntityTypeBuilder<ScreenResolution> builder)
        {
            builder.ToTable("ScreenResolutions");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(s => s.TVAttributes)
                   .WithOne(s => s.ScreenResolution)
                   .HasForeignKey(s => s.ScreenResolutionId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        } 
    }
}
