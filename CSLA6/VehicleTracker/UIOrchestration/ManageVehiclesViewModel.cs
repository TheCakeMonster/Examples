using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Objects;
using VehicleTracker.UIOrchestration.Navigation;

// Generated from the built-in Scriban ViewModel template

namespace VehicleTracker.UIOrchestration
{

	public class ManageVehiclesViewModel
	{

		private readonly INavigator _navigator;
		private readonly IVehicleFactory _vehicleFactory;
		private readonly IVehicleListFactory _vehicleListFactory;

		#region Constructors

		public ManageVehiclesViewModel(INavigator navigator, IVehicleFactory vehicleFactory, IVehicleListFactory vehicleListFactory)
		{
			_navigator = navigator;
			_vehicleFactory = vehicleFactory;
			_vehicleListFactory = vehicleListFactory;
		}

		#endregion

		#region Enums

		public enum AvailableViews : byte
		{
			Loading = 0,
			List = 1,
			EditModel = 2
		}

		#endregion

		#region Exposed Properties

		public Action OnViewChanged { get; set; }

		public bool IsLoaded { get { return !(CurrentView == AvailableViews.Loading); } }

		public AvailableViews CurrentView { get; private set; } = AvailableViews.Loading;

		public VehicleList List { get; private set; }

		public Vehicle Model { get; private set; }

		#endregion

		#region Exposed Methods

		public async Task InitialiseAsync()
		{
			// Load the list of items to display
			List = await VehicleList_LoadAsync();

			// Change mode to display the results
			ChangeView(AvailableViews.List);
		}

		#region Vehicle Edit

		public async Task BeginAddModelAsync()
		{
			Model = await Vehicle_CreateAsync();

			// Change view to support editing of the model
			ChangeView(AvailableViews.EditModel);
		}

		public async Task BeginEditModelAsync(int vehicleId)
		{
			Model = await Vehicle_LoadAsync(vehicleId);

			// Change view to support editing of the model
			ChangeView(AvailableViews.EditModel);
		}

		public async Task AcceptModelEditAsync()
		{
			// Save the object into the database
			await Model.SaveAndMergeAsync();

			// Remove the reference to the it from memory
			Model = null;
			
			// Go back to the data structures list view
			ChangeView(AvailableViews.Loading);
			List = await VehicleList_LoadAsync();
			ChangeView(AvailableViews.List);
		}

		public void CancelModelEdit()
		{
			// Remove the reference to the data structure
			Model = null;

			// Discard the changes by changing back to the previous view
			ChangeView(AvailableViews.List);
		}

		public async Task DeleteModelAsync(int vehicleId)
		{
			// Initiate the data access operation to perform deletion
			await Vehicle_DeleteAsync(vehicleId);

			// Reload the list of datastructures to show the changes made
			ChangeView(AvailableViews.Loading);
			List = await VehicleList_LoadAsync();
			ChangeView(AvailableViews.List);
		}

		#endregion

		#region Navigation

		public void CancelList()
		{
			_navigator.NavigateTo(Routes.Home);
		}

		#endregion

		#endregion

		#region Data Access

		protected virtual Task<VehicleList> VehicleList_LoadAsync()
		{
			// Load the list of items present for display
			return _vehicleListFactory.GetVehicleListAsync();
		}

		protected virtual async Task<Vehicle> Vehicle_CreateAsync()
		{
			return await _vehicleFactory.NewVehicleAsync();
		}

		protected virtual async Task<Vehicle> Vehicle_LoadAsync(int vehicleId)
		{
			return await _vehicleFactory.GetVehicleAsync(vehicleId);
		}

		protected virtual async Task Vehicle_DeleteAsync(int vehicleId)
		{
			await _vehicleFactory.DeleteVehicleAsync(vehicleId);
		}

		#endregion

		#region Private Helper Methods

		private void ChangeView(AvailableViews newMode)
		{
			CurrentView = newMode;

			// Signal to any consumers that the view changed
			OnViewChanged?.Invoke();
		}

		#endregion

	}
}
