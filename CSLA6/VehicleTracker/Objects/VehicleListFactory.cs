using Csla;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTracker.Objects
{

	/// <summary>
	/// Factory class that provides strongly-typed methods for instantiating a VehicleList
	/// </summary>
	public class VehicleListFactory : IVehicleListFactory
	{

		private readonly IDataPortal<VehicleList> _dataPortal;

		public VehicleListFactory(IDataPortal<VehicleList> dataPortal)
		{
			_dataPortal = dataPortal;
		}

		/// <summary>
		/// Get a list of Vehicles from the data store
		/// </summary>
		/// <returns>The requested list of Vehicle objects</returns>
		public async Task<VehicleList> GetVehicleListAsync()
		{
			return await _dataPortal.FetchAsync();
		}

	}
}
