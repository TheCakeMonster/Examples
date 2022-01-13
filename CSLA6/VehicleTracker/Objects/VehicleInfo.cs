using System;
using System.Threading.Tasks;
using Csla;
using VehicleTracker.Objects.DataAccess;

// Generated from the built-in Scriban CSLA ReadOnlyChild template

namespace VehicleTracker.Objects
{
	[Serializable]
	public class VehicleInfo : ReadOnlyBase<VehicleInfo>
	{

		private static readonly PropertyInfo<int> _vehicleIdProperty = RegisterProperty<int>(nameof(VehicleId));
		private static readonly PropertyInfo<string> _nickNameProperty = RegisterProperty<string>(nameof(NickName));
		private static readonly PropertyInfo<string> _keyReferenceProperty = RegisterProperty<string>(nameof(KeyReference));
		
		#region Exposed Properties and Methods

		public int VehicleId
		{
			get { return GetProperty(_vehicleIdProperty); }
		}

		public string NickName
		{
			get { return GetProperty(_nickNameProperty); }
		}

		public string KeyReference
		{
			get { return GetProperty(_keyReferenceProperty); }
		}

		#endregion

		#region Instantiation

		public VehicleInfo() 
		{
			// Public constructor is required for CSLA to work, but should never be used!
		}

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(VehicleDTO data)
		{
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(VehicleDTO data)
		{
			LoadProperty(_vehicleIdProperty, data.VehicleId);
			LoadProperty(_nickNameProperty, data.NickName);
			LoadProperty(_keyReferenceProperty, data.KeyReference);
			// Complete the load by requesting children load themselves

			return Task.CompletedTask;
		}

		#endregion

	}
}