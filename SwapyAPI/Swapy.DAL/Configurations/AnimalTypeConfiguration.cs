using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AnimalTypeConfiguration : IEntityTypeConfiguration<AnimalType>
    {
        public void Configure(EntityTypeBuilder<AnimalType> builder)
        {
            builder.ToTable("AnimalTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);
             
            builder 
                .HasMany(x => x.AnimalBreeds)
                .WithOne(x => x.AnimalType)
                .OnDelete(DeleteBehavior.SetNull);
        }
    } 
}
  