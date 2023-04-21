using Swapy.DAL.Entities;
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
    }
}