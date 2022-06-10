using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using ProjectTracker.Objects;
using ProjectTracker.UIControl.Navigation;

// Generated from the built-in Scriban CSLA 6.0 ViewModel template

namespace ProjectTracker.UIControl
{

	public class ManageProjectsViewModel
	{

		private readonly INavigator _navigator;
		private readonly ProjectList.Factory _listFactory;
		private readonly ProjectEdit.Factory _modelFactory;

		#region Constructors

		public ManageProjectsViewModel(
			INavigator navigator, 
			ProjectList.Factory listFactory, 
			ProjectEdit.Factory modelFactory
			)
		{
			_navigator = navigator;
			_listFactory = listFactory;
			_modelFactory = modelFactory;
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

		public Action? OnViewChanged { get; set; }

		public bool IsLoaded { get { return !(CurrentView == AvailableViews.Loading); } }

		public AvailableViews CurrentView { get; private set; } = AvailableViews.Loading;

		public ProjectList? List { get; private set; }

		public ProjectEdit? Model { get; private set; }

		#endregion

		#region Exposed Methods

		public async Task InitialiseAsync()
		{
			// Load the list of items to display
			List = await ProjectList_LoadAsync();

			// Change mode to display the results
			ChangeView(AvailableViews.List);
		}

		#region ProjectEdit Edit

		public async Task BeginAddModelAsync()
		{
			Model = await ProjectEdit_CreateAsync();

			// Change view to support editing of the model
			ChangeView(AvailableViews.EditModel);
		}

		public async Task BeginEditModelAsync(int id)
		{
			Model = await ProjectEdit_LoadAsync(id);

			// Change view to support editing of the model
			ChangeView(AvailableViews.EditModel);
		}

		public async Task AcceptModelEditAsync()
		{
			// Save the object into the database
			await ProjectEdit_SaveAndMergeAsync();

			// Remove the reference to the it from memory
			Model = null;
			
			// Go back to the list view
			ChangeView(AvailableViews.Loading);
			List = await ProjectList_LoadAsync();
			ChangeView(AvailableViews.List);
		}

		public void CancelModelEdit()
		{
			// Remove the reference to the model
			Model = null;

			// Discard the changes by changing back to the previous view
			ChangeView(AvailableViews.List);
		}

		public async Task DeleteModelAsync(int id)
		{
			// Initiate the data access operation to perform deletion
			await ProjectEdit_DeleteAsync(id);

			// Reload the list of items to show the changes made
			ChangeView(AvailableViews.Loading);
			List = await ProjectList_LoadAsync();
			ChangeView(AvailableViews.List);
		}

		public Task ValidationFailedAsync()
		{
			string failureMessage;

			if (Model is null) return Task.CompletedTask;
			failureMessage = BuildValidationFailureMessage(Model);
			System.Diagnostics.Debug.WriteLine(failureMessage);

			return Task.CompletedTask;
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

		protected virtual Task<ProjectList> ProjectList_LoadAsync()
		{
			// Load the list of items present for display
			return _listFactory.GetProjectListAsync();
		}

		protected virtual async Task<ProjectEdit> ProjectEdit_CreateAsync()
		{
			return await _modelFactory.NewProjectEditAsync();
		}

		protected virtual async Task<ProjectEdit> ProjectEdit_LoadAsync(int id)
		{
			return await _modelFactory.GetProjectEditAsync(id);
		}

		protected virtual async Task ProjectEdit_SaveAndMergeAsync()
		{
			if (Model is null) return;
			await Model.SaveAndMergeAsync();
		}

		protected virtual async Task ProjectEdit_DeleteAsync(int id)
		{
			await _modelFactory.DeleteProjectEditAsync(id);
		}

		#endregion

		#region Private Helper Methods

		private void ChangeView(AvailableViews newMode)
		{
			CurrentView = newMode;

			// Signal to any consumers that the view changed
			OnViewChanged?.Invoke();
		}


		private string BuildValidationFailureMessage(Csla.Core.BusinessBase businessObject)
		{
			Csla.Rules.BrokenRulesCollection brokenRules;
			StringBuilder stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("Validation failure identified during edit submission!");

			brokenRules = businessObject.GetBrokenRules();
			if (brokenRules is null || brokenRules.Count < 1)
			{
				stringBuilder.AppendLine("Unidentified broken rules in child objects!");
				return stringBuilder.ToString();
			}

			foreach (Csla.Rules.BrokenRule brokenRule in brokenRules)
			{
				stringBuilder.AppendLine(brokenRule.Description);
			}

			return stringBuilder.ToString();
		}

		#endregion

	}
}
