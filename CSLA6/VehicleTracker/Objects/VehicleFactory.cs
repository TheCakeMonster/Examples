using Csla;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Objects
{

	/// <summary>
	/// Factory class that provides strongly-typed methods for instantiating a Vehicle
	/// </summary>
	public class VehicleFactory : IVehicleFactory
	{
		private readonly IDataPortal<Vehicle> _dataPortal;

		public VehicleFactory(IDataPortal<Vehicle> dataPortal)
		{
			_dataPortal = dataPortal;
		}

		/// <summary>
		/// Create a new Vehicle with appropriate default values
		/// </summary>
		/// <returns>A new Vehicle</returns>
		public async Task<Vehicle> NewVehicleAsync()
		{
			return await _dataPortal.CreateAsync();
		}

		/// <summary>
		/// Get an existing Vehicle from the data store
		/// </summary>
		/// <returns>The requested Vehicle</returns>
		public async Task<Vehicle> GetVehicleAsync(int vehicleId)
		{
			return await _dataPortal.FetchAsync(new Vehicle.Criteria(vehicleId));
		}

		/// <summary>
		/// Deletes a single Vehicle from the data store
		/// </summary>
		public async Task DeleteVehicleAsync(int vehicleId)
		{
			await _dataPortal.DeleteAsync(new Vehicle.Criteria(vehicleId));
		}

	}
}
