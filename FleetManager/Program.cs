using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

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

                var controller = scope.ServiceProvider.GetService<VehicleController>();

                controller.Show();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var baseDirectory = AppContext.BaseDirectory;

            // Database
            services.AddDbContext<Data.Entity.FleetContext>(options => options.UseSqlite($"Data source={Path.Combine(baseDirectory, "fleet.db")}"));

            // Domain
            services.AddSingleton<Domain.IVehicleService, Domain.Concrete.VehicleService>();
            services.AddSingleton<Model.Validation.IChassiUniqueValidationService, Domain.Concrete.ChassiUniqueValidationService>();

            // Data
            services.AddSingleton<Data.IVehicleRepository, Data.Concrete.VehicleRepository>();

            // Controller
            services.AddSingleton<VehicleController>();
        }
    }
}
