using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using Vega.Models;
using Vega.Persistence.Interfaces;

namespace Vega.Persistence.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext _dbContext;
        public VehicleRepository(VegaDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<Vehicle> GetVehicle(int id, bool vehicleisAvailable = true)
        {
            if (vehicleisAvailable != false)
                return await _dbContext.Vehicles.FindAsync(id);



            return await _dbContext.Vehicles
              .Include(v => v.Features)
               .ThenInclude(vf => vf.Feature)
              .Include(v => v.Model)
              .ThenInclude(m => m.Make)
               .SingleOrDefaultAsync(v => v.Id == id);

        }
        public void Add(Vehicle vehicle)
        { 
           _dbContext.Vehicles.Add(vehicle);
        
        }
        public void Remove(Vehicle vehicle)
        {
            _dbContext.Remove(vehicle);
        }

        
    }
}
