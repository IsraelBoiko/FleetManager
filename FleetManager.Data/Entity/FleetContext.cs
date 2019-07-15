using FleetManager.Model;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Data.Entity
{
    public class FleetContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=fleet.db");
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
