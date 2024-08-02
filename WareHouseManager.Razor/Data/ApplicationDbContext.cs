using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WareHouseManager.Razor.Models;


namespace WareHouseManager.Razor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Production> Productions { get; set; }

        public DbSet<Store> Stores { get; set; }

        //public DbSet<StoreHistory> storeHistories { get; set; }
        
    }
}
