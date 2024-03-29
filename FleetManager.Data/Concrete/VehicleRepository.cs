﻿using FleetManager.Data.Entity;
using FleetManager.Model;
using System.Linq;

namespace FleetManager.Data.Concrete
{
    public class VehicleRepository : IVehicleRepository
    {
        public VehicleRepository(FleetContext dbContext)
        {
            DbContext = dbContext;
        }

        public FleetContext DbContext { get; }

        public void Add(Vehicle model)
        {
            DbContext.Vehicles.Add(model);

            DbContext.SaveChanges();
        }

        public IQueryable<Vehicle> All() => DbContext.Vehicles;

        public bool ChassiExists(string chassi) => DbContext.Vehicles
            .Any(v => v.Chassi == chassi);

        public Vehicle ChassiFind(string chassi) => DbContext.Vehicles
            .SingleOrDefault(v => v.Chassi == chassi);

        public void Remove(Vehicle model)
        {
            DbContext.Vehicles.Remove(model);

            DbContext.SaveChanges();
        }

        public void Update(Vehicle model) => DbContext.SaveChanges();
    }
}
