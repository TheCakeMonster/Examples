using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// Generated from the built-in Scriban RepositoryInterface template

namespace VehicleTracker.Objects.DataAccess
{
	public interface IVehicleRepository
	{
		
		Task<IList<VehicleDTO>> FetchListAsync();

		Task<VehicleDTO> FetchAsync(int vehicleId);

		Task<VehicleDTO> InsertAsync(VehicleDTO data);

		Task<VehicleDTO> UpdateAsync(VehicleDTO data);

		Task DeleteAsync(int vehicleId);

	}
}