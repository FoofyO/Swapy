using Swapy.Common.Entities;
using Swapy.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Swapy.DAL
{
    public class SwapyDbContext : IdentityDbContext<IdentityUser>
    {
        public SwapyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimalAttributeConfiguration());
            builder.ApplyConfiguration(new AnimalBreedConfiguration());
            builder.ApplyConfiguration(new AutoAttributeConfiguration());
            builder.ApplyConfiguration(new AutoBrandConfiguration());
            builder.ApplyConfiguration(new AutoBrandTypeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new ClothesAttributeConfiguration());
            builder.ApplyConfiguration(new ClothesBrandConfiguration());
            builder.ApplyConfiguration(new ClothesBrandViewConfiguration());
            builder.ApplyConfiguration(new ClothesSeasonConfiguration());
            builder.ApplyConfiguration(new ClothesSizeConfiguration());
            builder.ApplyConfiguration(new ClothesViewConfiguration());
            builder.ApplyConfiguration(new ColorConfiguration());
            builder.ApplyConfiguration(new Configurations.ModelConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new ElectronicAttributeConfiguration());
            builder.ApplyConfiguration(new ElectronicBrandConfiguration());
            builder.ApplyConfiguration(new ElectronicBrandTypeConfiguration());
            builder.ApplyConfiguration(new FuelTypeConfiguration());
            builder.ApplyConfiguration(new GenderConfiguration());
            builder.ApplyConfiguration(new ItemAttributeConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new MemoryConfiguration());
            builder.ApplyConfiguration(new MemoryModelConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new ModelColorConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new RealEstateAttributeConfiguration());
            builder.ApplyConfiguration(new ScreenDiagonalConfiguration());
            builder.ApplyConfiguration(new ScreenResolutionConfiguration());
            builder.ApplyConfiguration(new SubcategoryConfiguration());
            builder.ApplyConfiguration(new SubscribeConfiguration());
            builder.ApplyConfiguration(new TVAttributeConfiguration());
            builder.ApplyConfiguration(new TVBrandConfiguration());
            builder.ApplyConfiguration(new TVTypeConfiguration());
            builder.ApplyConfiguration(new TransmissionTypeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<AnimalAttribute> AnimalAttributes { get; set; }
        public DbSet<AnimalBreed> AnimalBreeds { get; set; }
        public DbSet<AutoAttribute> AutoAttributes { get; set; }
        public DbSet<AutoBrand> AutoBrands { get; set; }
        public DbSet<AutoBrandType> AutoBrandsTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ClothesAttribute> ClothesAttributes { get; set; }
        public DbSet<ClothesBrand> ClothesBrands { get; set; }
        public DbSet<ClothesBrandView> ClothesBrandsViews { get; set; }
        public DbSet<ClothesSeason> ClothesSeasons { get; set; }
        public DbSet<ClothesSize> ClothesSizes { get; set; }
        public DbSet<ClothesView> ClothesViews { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ElectronicAttribute> ElectronicAttributes { get; set; }
        public DbSet<ElectronicBrand> ElectronicBrands { get; set; }
        public DbSet<ElectronicBrandType> ElectronicBrandsTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MemoryModel> MemoriesModels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<ModelColor> ModelsColors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<RealEstateAttribute> RealEstateAttributes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ScreenDiagonal> ScreenDiagonals { get; set; }
        public DbSet<ScreenResolution> ScreenResolutions { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<TVAttribute> TVAttributes { get; set; }
        public DbSet<TVBrand> TVBrands { get; set; }
        public DbSet<TVType> TVTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}