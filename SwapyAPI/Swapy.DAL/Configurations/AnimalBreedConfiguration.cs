using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AnimalBreedConfiguration : IEntityTypeConfiguration<AnimalBreed>
    {
        public void Configure(EntityTypeBuilder<AnimalBreed> builder)
        {
            builder.ToTable("AnimalBreedsTypes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");
             
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(32);
              
            builder
                .HasOne(x => x.AnimalType)
                .WithMany(x => x.AnimalBreeds)
                .HasForeignKey(p => p.AnimalTypeId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.AnimalAttributes)
                .WithOne(x => x.AnimalBreed) 
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
