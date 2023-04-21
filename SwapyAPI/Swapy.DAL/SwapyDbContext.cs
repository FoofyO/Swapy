using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            //modelBuilder.ApplyConfiguration<Task>(new TaskConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
