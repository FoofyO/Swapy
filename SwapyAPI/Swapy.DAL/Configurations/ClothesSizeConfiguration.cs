namespace Swapy.DAL.Configurations
{
    public class ClothesSizeConfiguration : IEntityTypeConfiguration<ClothesSize>
    {
        public void Configure(EntityTypeBuilder<ClothesSize> builder)
        {
            builder.ToTable("ClothesSizes");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.IsShoe)
                .IsRequired() 
                .HasColumnType("BIT");

            builder.Property(x => x.IsChild)
                .IsRequired() 
                .HasColumnType("BIT");
 
            builder.Property(i => i.Size)
                   .HasColumnType("NVARCHAR(32)")
                   .HasMaxLength(32)
                   .IsRequired();  

            builder.HasOne(i => i.ClothesAttribute)  
                   .WithMany(p => p.ClothesSize)
                   .HasForeignKey(i => i.ClothesSizeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();       
        }  
    }
} 
