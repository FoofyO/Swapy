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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AutoAttributes> AutoAttributes { get; set; }
        public DbSet<AutoBrands> AutoBrands { get; set; }
        public DbSet<AutoBrandsTypes> AutoBrandsTypes { get; set; }
        public DbSet<AutoColors> AutoColors { get; set; }
        public DbSet<AutoTypes> AutoTypes { get; set; }
        public DbSet<FuelTypes> FuelTypes { get; set; }
        public DbSet<TransmissionTypes> TransmissionTypes { get; set; }
    }
}
