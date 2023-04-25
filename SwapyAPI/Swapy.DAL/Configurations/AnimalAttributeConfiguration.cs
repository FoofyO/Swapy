using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class AnimalAttributeConfiguration : IEntityTypeConfiguration<AnimalAttribute>
    {
        public void Configure(EntityTypeBuilder<AnimalAttribute> builder)
        {
            builder.ToTable("AnimalAttributes"); 
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");
                
            builder
               .HasOne(x => x.AnimalBreeds)
               .WithMany(x => x.AnimalAttributes)
               .HasForeignKey(p => p.AnimalBreedId)
               .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(x => x.Product)
               .WithOne(x => x.AnimalAttribute)
               .HasForeignKey<Product>(x => x.AnimalAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
