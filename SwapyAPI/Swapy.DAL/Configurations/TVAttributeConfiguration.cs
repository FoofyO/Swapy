using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class TVAttributeConfiguration : IEntityTypeConfiguration<TVAttribute>
    {
        public void Configure(EntityTypeBuilder<TVAttribute> builder)
        {
            builder.ToTable("TVAttributes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");
             
            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");
            
            builder.Property(x => x.IsSmart)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.TVBrand) 
                .WithMany(x => x.TVAttributes)
                .HasForeignKey(p => p.TVBrandId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.TVType)
                .WithMany(x => x.TVAttributes)
                .HasForeignKey(p => p.TVTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder 
                .HasOne(x => x.ScreenResolution)
                .WithMany(x => x.TVAttributes) 
                .HasForeignKey(p => p.ScreenResolutionId)
                .OnDelete(DeleteBehavior.Cascade);
             
            builder
               .HasOne(x => x.ScreenDiagonal) 
               .WithMany(x => x.TVAttributes)
               .HasForeignKey(p => p.ScreenDiagonalId)
               .OnDelete(DeleteBehavior.Cascade);
             
            builder 
               .HasOne(x => x.Product)
               .WithOne(x => x.TVAttribute)
               .HasForeignKey<Product>(x => x.TVAttributeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
 