using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TVTypeConfiguration : IEntityTypeConfiguration<TVType>
    {
        public void Configure(EntityTypeBuilder<TVType> builder)
        {
            builder.ToTable("TVTypes");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(t => t.TVAttributes)
                   .WithOne(t => t.TVType)
                   .HasForeignKey(t => t.TVTypeId)
                   .IsRequired(false);
        }
    }
}
