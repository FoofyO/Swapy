using Swapy.DAL.Entities;

namespace Swapy.DAL.Configurations
{
    public class ClothesAttributeConfiguration : IEntityTypeConfiguration<ClothesAttribute>
    {
        public void Configure(EntityTypeBuilder<ClothesAttribute> builder)
        {
            builder.ToTable("ClothesAttributes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsNew)
                .IsRequired()
                .HasColumnType("BIT");

            builder.HasOne(p => p.Season)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.SeasonId) 
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
             
            builder.HasOne(p => p.ClothesSize)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.ClothesSizeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
            
            builder.HasOne(p => p.ClothesTypeView)
                   .WithMany(u => u.ClothesAttributes)
                   .HasForeignKey(p => p.ClothesTypeViewId)
                   .OnDelete(DeleteBehavior.Cascade) 
                   .IsRequired();
             
            builder
               .HasOne(x => x.Product)
               .WithOne(x => x.ClothesAttribute)
               .HasForeignKey<Product>(x => x.ClothesAttributeId)
               .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
