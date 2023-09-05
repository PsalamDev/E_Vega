using Vega.Models;

namespace Vega.Persistence.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool vehicleisAvailable = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}
