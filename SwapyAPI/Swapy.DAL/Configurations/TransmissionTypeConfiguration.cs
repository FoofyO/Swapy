using Swapy.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Swapy.DAL.Configurations
{
    public class TransmissionTypeConfiguration : IEntityTypeConfiguration<TransmissionType>
    {
        public void Configure(EntityTypeBuilder<TransmissionType> builder)
        {
            builder.ToTable("TransmissionTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.HasMany(a => a.Names)
                   .WithOne()
                   .IsRequired(false);

            builder.HasMany(x => x.AutoAttributes)
                   .WithOne(x => x.TransmissionType)
                   .HasForeignKey(x => x.TransmissionTypeId)
                   .IsRequired(false);
        }
    }
}
