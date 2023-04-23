using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Swapy.DAL.Configurations;
using Swapy.DAL.Entities;

namespace Swapy.DAL
{
    public class SwapyDbContext : IdentityDbContext<IdentityUser>
    {
        public SwapyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
<<<<<<< Updated upstream
            modelBuilder.ApplyConfiguration<AutoAttributes>(new AutoAttributesConfiguration());
            modelBuilder.ApplyConfiguration<AutoBrands>(new AutoBrandsConfiguration());
            modelBuilder.ApplyConfiguration<AutoBrandsTypes>(new AutoBrandsTypesConfiguration());
            modelBuilder.ApplyConfiguration<AutoColors>(new AutoColorsConfiguration());
            modelBuilder.ApplyConfiguration<AutoTypes>(new AutoTypesConfiguration());
            modelBuilder.ApplyConfiguration<FuelTypes>(new FuelTypesConfiguration());
            modelBuilder.ApplyConfiguration<TransmissionTypes>(new TransmissionTypesConfiguration());
            modelBuilder.ApplyConfiguration<Colors>(new ColorsConfiguration());
            modelBuilder.ApplyConfiguration<ElectronicAttributes>(new ElectronicAttributesConfiguration());
            modelBuilder.ApplyConfiguration<ElectronicBrands>(new ElectronicBrandsConfiguration());
            modelBuilder.ApplyConfiguration<ElectronicBrandsTypes>(new ElectronicBrandsTypesConfiguration());
            modelBuilder.ApplyConfiguration<ElectronicTypes>(new ElectronicTypesConfiguration());
            modelBuilder.ApplyConfiguration<Memories>(new MemoriesConfiguration());
            modelBuilder.ApplyConfiguration<MemoriesModels>(new MemoriesModelsConfiguration());
            modelBuilder.ApplyConfiguration<Models>(new ModelsConfiguration());
            modelBuilder.ApplyConfiguration<ModelsColors>(new ModelsColorsConfiguration());
            modelBuilder.ApplyConfiguration<ItemAttributes>(new ItemAttributesConfiguration());
            modelBuilder.ApplyConfiguration<ItemTypes>(new ItemTypesConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AutoAttributes> AutoAttributes { get; set; }
        public DbSet<AutoBrands> AutoBrands { get; set; }
        public DbSet<AutoBrandsTypes> AutoBrandsTypes { get; set; }
        public DbSet<AutoColors> AutoColors { get; set; }
        public DbSet<AutoTypes> AutoTypes { get; set; }
        public DbSet<FuelTypes> FuelTypes { get; set; }
        public DbSet<TransmissionTypes> TransmissionTypes { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<ElectronicAttributes> ElectronicAttributes { get; set; }
        public DbSet<ElectronicBrands> ElectronicBrands { get; set; }
        public DbSet<ElectronicBrandsTypes> ElectronicBrandsTypes { get; set; }
        public DbSet<ElectronicTypes> ElectronicTypes { get; set; }
        public DbSet<Memories> Memories { get; set; }
        public DbSet<MemoriesModels> MemoriesModels { get; set; }
        public DbSet<Models> Models { get; set; }
        public DbSet<ModelsColors> ModelsColors { get; set; }
        public DbSet<ItemAttributes> ItemAttributes { get; set; }
        public DbSet<ItemTypes> ItemTypes { get; set; }
=======
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new SubscribeConfiguration());
            builder.ApplyConfiguration(new SubcategoryConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration<AutoAttribute>(new AutoAttributeConfiguration());
            builder.ApplyConfiguration<AutoBrand>(new AutoBrandConfiguration());
            builder.ApplyConfiguration<AutoBrandType>(new AutoBrandTypeConfiguration());
            builder.ApplyConfiguration<AutoColor>(new AutoColorConfiguration());
            builder.ApplyConfiguration<AutoType>(new AutoTypeConfiguration());
            builder.ApplyConfiguration<FuelType>(new FuelTypeConfiguration());
            builder.ApplyConfiguration<TransmissionType>(new TransmissionTypeConfiguration());
            builder.ApplyConfiguration<Color>(new ColorConfiguration());
            builder.ApplyConfiguration<ElectronicAttribute>(new ElectronicAttributeConfiguration());
            builder.ApplyConfiguration<ElectronicBrand>(new ElectronicBrandConfiguration());
            builder.ApplyConfiguration<ElectronicBrandType>(new ElectronicBrandTypeConfiguration());
            builder.ApplyConfiguration<ElectronicType>(new ElectronicTypeConfiguration());
            builder.ApplyConfiguration<Memory>(new MemoryConfiguration());
            builder.ApplyConfiguration<MemoryModel>(new MemoryModelConfiguration());
            builder.ApplyConfiguration<Model>(new ModelConfiguration());
            builder.ApplyConfiguration<ModelColor>(new ModelColorConfiguration());
            builder.ApplyConfiguration<ItemAttribute>(new ItemAttributeConfiguration());
            builder.ApplyConfiguration<ItemType>(new ItemTypeConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AutoAttribute> AutoAttributes { get; set; }
        public DbSet<AutoBrand> AutoBrands { get; set; }
        public DbSet<AutoBrandType> AutoBrandsTypes { get; set; }
        public DbSet<AutoColor> AutoColors { get; set; }
        public DbSet<AutoType> AutoTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ElectronicAttribute> ElectronicAttributes { get; set; }
        public DbSet<ElectronicBrand> ElectronicBrands { get; set; }
        public DbSet<ElectronicBrandType> ElectronicBrandsTypes { get; set; }
        public DbSet<ElectronicType> ElectronicTypes { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MemoryModel> MemoriesModels { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelColor> ModelsColors { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
>>>>>>> Stashed changes
    }
}
