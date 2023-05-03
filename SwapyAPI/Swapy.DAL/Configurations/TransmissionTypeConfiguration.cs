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

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(32);

            builder.HasMany(x => x.AutoAttributes)
                   .WithOne(x => x.TransmissionType)
                   .HasForeignKey(x => x.TransmissionTypeId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);
        }
    }
}
