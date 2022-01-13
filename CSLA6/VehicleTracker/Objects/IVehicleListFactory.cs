using System.Threading.Tasks;

namespace VehicleTracker.Objects
{
	public interface IVehicleListFactory
	{
		Task<VehicleList> GetVehicleListAsync();
	}
}