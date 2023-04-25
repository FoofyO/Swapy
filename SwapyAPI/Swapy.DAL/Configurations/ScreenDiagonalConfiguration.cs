using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ScreenDiagonalConfiguration : IEntityTypeConfiguration<ScreenDiagonal>
    {
        public void Configure(EntityTypeBuilder<ScreenDiagonal> builder)
        {
            builder.ToTable("ScreenDiagonals");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);

            builder
                .HasMany(x => x.TVAttributes)
                .WithOne(x => x.ScreenDiagonal)
                .HasForeignKey(x => x.ScreenDiagonalId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
