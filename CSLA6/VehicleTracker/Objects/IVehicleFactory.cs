using System.Threading.Tasks;

namespace VehicleTracker.Objects
{
	public interface IVehicleFactory
	{
		Task DeleteVehicleAsync(int vehicleId);
		Task<Vehicle> GetVehicleAsync(int vehicleId);
		Task<Vehicle> NewVehicleAsync();
	}
}