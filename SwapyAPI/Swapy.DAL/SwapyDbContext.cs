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
    }
}
