using FleetManager.Model;
using Microsoft.EntityFrameworkCore;

namespace FleetManager.Data.Entity
{
    public class FleetContext : DbContext
    {
        public FleetContext(DbContextOptions<FleetContext> options)
            : base(options)
        {
        }

        public FleetContext()
            : this(new DbContextOptionsBuilder<FleetContext>().UseSqlite("Data source=vehicle.db").Options)
        {
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Map.VehicleMap());
        }
    }
}
