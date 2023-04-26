using Swapy.DAL.Entities;
using Swapy.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Swapy.DAL
{
    public class SwapyDbContext : IdentityDbContext<IdentityUser>
    {
        public SwapyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
<<<<<<< HEAD
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
            modelBuilder.ApplyConfiguration<RealEstatesAttributes>(new RealEstatesAttributesConfiguration());
            modelBuilder.ApplyConfiguration<RealEstateTypes>(new RealEstateTypesConfiguration());
            modelBuilder.ApplyConfiguration<AnimalAttributes>(new AnimalAttributesConfiguration());
            modelBuilder.ApplyConfiguration<AnimalBreeds>(new AnimalBreedsConfiguration());
            modelBuilder.ApplyConfiguration<AnimalTypes>(new AnimalTypesConfiguration());
            modelBuilder.ApplyConfiguration<TVAttributes>(new TVAttributesConfiguration());
            modelBuilder.ApplyConfiguration<TVBrands>(new TVBrandsConfiguration());
            modelBuilder.ApplyConfiguration<TVTypes>(new TVTypesConfiguration());
            modelBuilder.ApplyConfiguration<ScreenResolutions>(new ScreenResolutionsConfiguration());
            modelBuilder.ApplyConfiguration<ScreenDiagonals>(new ScreenDiagonalsConfiguration());
            modelBuilder.ApplyConfiguration<ClothesAttributes>(new ClothesAttributesConfiguration());
            modelBuilder.ApplyConfiguration<ClothesSizes>(new ClothesSizesConfiguration());
            modelBuilder.ApplyConfiguration<ClothesSeasons>(new ClothesSeasonsConfiguration());
            modelBuilder.ApplyConfiguration<ClothesBrandsViews>(new ClothesBrandsViewsConfiguration());
            modelBuilder.ApplyConfiguration<ClothesBrands>(new ClothesBrandsConfiguration());
            modelBuilder.ApplyConfiguration<ClothesViews>(new ClothesViewsConfiguration());
            modelBuilder.ApplyConfiguration<ClothesTypes>(new ClothesTypesConfiguration());
            modelBuilder.ApplyConfiguration<Genders>(new GendersConfiguration());

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
        public DbSet<RealEstatesAttributes> RealEstatesAttributes { get; set; }
        public DbSet<RealEstateTypes> RealEstateTypes { get; set; }
        public DbSet<AnimalAttributes> AnimalAttributes { get; set; }
        public DbSet<AnimalBreeds> AnimalBreeds { get; set; }
        public DbSet<AnimalTypes> AnimalTypes { get; set; }
        public DbSet<TVAttributes> TVAttributes { get; set; }
        public DbSet<TVBrands> TVBrands { get; set; }
        public DbSet<TVTypes> TVTypes { get; set; }
        public DbSet<ScreenResolutions> ScreenResolutions { get; set; }
        public DbSet<ScreenDiagonals> ScreenDiagonals { get; set; }
        public DbSet<ClothesAttributes> ClothesAttributes { get; set; }
        public DbSet<ClothesSizes> ClothesSizes { get; set; }
        public DbSet<ClothesSeasons> ClothesSeasons { get; set; }
        public DbSet<ClothesBrandsViews> ClothesBrandsViews { get; set; }
        public DbSet<ClothesBrands> ClothesBrands { get; set; }
        public DbSet<ClothesViews> ClothesViews { get; set; }
        public DbSet<ClothesTypes> ClothesTypes { get; set; }
        public DbSet<Genders> Genders { get; set; }
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
            builder.ApplyConfiguration<RealEstatesAttribute>(new RealEstatesAttributeConfiguration());
            builder.ApplyConfiguration<RealEstateType>(new RealEstateTypeConfiguration());
            builder.ApplyConfiguration<AnimalAttribute>(new AnimalAttributeConfiguration());
            builder.ApplyConfiguration<AnimalBreed>(new AnimalBreedConfiguration()); 
            builder.ApplyConfiguration<AnimalType>(new AnimalTypeConfiguration());
            builder.ApplyConfiguration<TVAttribute>(new TVAttributeConfiguration());
            builder.ApplyConfiguration<TVBrand>(new TVBrandConfiguration());
            builder.ApplyConfiguration<TVType>(new TVTypeConfiguration());
            builder.ApplyConfiguration<ScreenResolution>(new ScreenResolutionConfiguration());
            builder.ApplyConfiguration<ScreenDiagonal>(new ScreenDiagonalConfiguration());
            builder.ApplyConfiguration<ClothesAttribute>(new ClothesAttributeConfiguration());
            builder.ApplyConfiguration<ClothesSizes>(new ClothesSizeConfiguration()); 
            builder.ApplyConfiguration<ClothesSeason>(new ClothesSeasonConfiguration()); 
            builder.ApplyConfiguration<ClothesBrandView>(new ClothesBrandViewConfiguration()); 
            builder.ApplyConfiguration<ClothesBrand>(new ClothesBrandConfiguration()); 
            builder.ApplyConfiguration<ClothesView>(new ClothesViewConfiguration()); 
            builder.ApplyConfiguration<ClothesType>(new ClothesTypeConfiguration()); 
            builder.ApplyConfiguration<Gender>(new GenderConfiguration());  
                
               
            base.OnModelCreating(builder);    
        }    
          
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
            base.OnModelCreating(builder);
        } 
         
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
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
<<<<<<< HEAD
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
        public DbSet<RealEstateAttribute> RealEstatesAttributes { get; set; }
        public DbSet<RealEstateType> RealEstateTypes { get; set; }
        public DbSet<AnimalAttribute> AnimalAttributes { get; set; }
        public DbSet<AnimalBreed> AnimalBreeds { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<TVAttribute> TVAttributes { get; set; }
        public DbSet<TVBrand> TVBrands { get; set; }
        public DbSet<TVType> TVTypes { get; set; }
        public DbSet<ScreenResolution> ScreenResolutions { get; set; }
        public DbSet<ScreenDiagonal> ScreenDiagonals { get; set; }
        public DbSet<ClothesAttribute> ClothesAttributes { get; set; }
        public DbSet<ClothesSize> ClothesSizes { get; set; }
        public DbSet<ClothesSeason> ClothesSeasons { get; set; }
        public DbSet<ClothesBrandView> ClothesBrandViews { get; set; }
        public DbSet<ClothesBrand> ClothesBrands { get; set; }
        public DbSet<ClothesView> ClothesViews { get; set; }
        public DbSet<ClothesType> ClothesTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
          
          

>>>>>>> Stashed changes
=======
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0 

    }
}