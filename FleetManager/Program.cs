using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace FleetManager
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start(new ServiceCollection());
        }

        public void Start(IServiceCollection services)
        {
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            using (var dbContext = scope.ServiceProvider.GetService<Data.Entity.FleetContext>())
            {
                dbContext.Database.Migrate();

                if (!dbContext.Vehicles.Any(e => e.Chassi == "ABC"))
                {
                    dbContext.Vehicles.Add(new Model.Vehicle("ABC", Model.VehicleType.Bus, "Azul"));
                    dbContext.SaveChanges();
                }

                var all = dbContext.Vehicles.ToList();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var baseDirectory = AppContext.BaseDirectory;

            services.AddDbContext<Data.Entity.FleetContext>(options => options.UseSqlite($"Data source={Path.Combine(baseDirectory, "fleet.db")}"));
        }
    }
}
