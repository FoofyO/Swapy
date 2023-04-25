using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ScreenResolutionConfiguration : IEntityTypeConfiguration<ScreenResolution>
    {
        public void Configure(EntityTypeBuilder<ScreenResolution> builder)
        {
            builder.ToTable("ScreenResolutions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired() 
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.TVAttributes)
                .WithOne(x => x.ScreenResolution)
                .HasForeignKey(x => x.ScreenResolutionId)
                .OnDelete(DeleteBehavior.SetNull);
        } 
    }
}
