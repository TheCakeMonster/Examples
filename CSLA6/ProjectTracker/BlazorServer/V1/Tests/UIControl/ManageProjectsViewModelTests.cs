using Csla.Configuration;
using DotNotStandard.DependencyInjection.AutoDiscovery.Filters;
using DotNotStandard.UnitTesting.Csla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectTracker.Objects;
using ProjectTracker.Objects.DataContracts;
using ProjectTracker.UIControl.Navigation;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// Generated from the built-in Scriban CSLA 6 ViewModelTests template

namespace ProjectTracker.UIControl.UnitTests
{
	[TestClass]
	public class ManageProjectsViewModelTests
	{
		
		#region Test Initialisation

		private static TestDIContext _testDIContext;

		[ClassInitialize]
		public static void Initialise(TestContext testContext)
		{
			var claims = new List<Claim> { new Claim(ClaimTypes.Role, "ProjectManager") };
			var identity = new ClaimsIdentity(claims);
			var principal = new ClaimsPrincipal(identity);

			_testDIContext = new TestDIContextBuilder()
				.UsePrincipal(principal)
				.AddCsla(cfg => cfg.AddConsoleApp())
				.AddTransient<ProjectList.Factory>()
				.AddTransient<ProjectEdit.Factory>()
				.AddTransient<ManageProjectsViewModel>()
				.AddTransient<IProjectRepository, Fakes.FakeProjectRepository>()
				.AddTransient<INavigator, Fakes.FakeNavigator>()
				.Build();
		}

		#endregion

		#region Initial Status

		[TestMethod]
		public void ViewModel_AfterCreation_CurrentViewIsLoading()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.Loading;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region InitialiseAsync

		[TestMethod]
		public async Task InitialiseAsync_AfterCompletion_ListIsLoaded()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			await viewModel.InitialiseAsync();

			// Assert
			Assert.IsNotNull(viewModel.List);
		}

		[TestMethod]
		public async Task InitialiseAsync_AfterCompletion_CurrentViewIsList()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.List;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			await viewModel.InitialiseAsync();
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region BeginAddModelAsync

		[TestMethod]
		public async Task BeginAddModelAsync_AfterCompletion_ModelIsLoaded()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			await viewModel.BeginAddModelAsync();

			// Assert
			Assert.IsNotNull(viewModel.Model);
		}

		[TestMethod]
		public async Task BeginAddModelAsync_AfterCompletion_CurrentViewIsEditModel()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.EditModel;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			await viewModel.BeginAddModelAsync();
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region BeginEditModelAsync

		[TestMethod]
		public async Task BeginEditModelAsync_AfterCompletion_ModelIsLoaded()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			await viewModel.BeginEditModelAsync(1);

			// Assert
			Assert.IsNotNull(viewModel.Model);
		}

		[TestMethod]
		public async Task BeginEditModelAsync_AfterCompletion_CurrentViewIsEditModel()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.EditModel;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			await viewModel.BeginEditModelAsync(1);
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region AcceptModelEditAsync

		[TestMethod]
		public async Task AcceptModelEditAsync_AfterCompletion_ListIsLoaded()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			await viewModel.BeginAddModelAsync();
			AssignValidModelData(viewModel);
			await viewModel.AcceptModelEditAsync();

			// Assert
			Assert.IsNotNull(viewModel.List);
		}

		[TestMethod]
		public async Task AcceptModelEditAsync_AfterCompletion_CurrentViewIsList()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.List;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			await viewModel.BeginAddModelAsync();
			AssignValidModelData(viewModel);
			await viewModel.AcceptModelEditAsync();
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region CancelModelEdit

		[TestMethod]
		public void CancelModelEdit_AfterCompletion_ModelIsNull()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			viewModel.CancelModelEdit();

			// Assert
			Assert.IsNull(viewModel.Model);
		}

		[TestMethod]
		public void CancelModelEdit_AfterCompletion_CurrentViewIsList()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.List;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			viewModel.CancelModelEdit();
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region DeleteModelAsync

		[TestMethod]
		public async Task DeleteModelAsync_AfterCompletion_ListIsLoaded()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();

			// Act
			await viewModel.DeleteModelAsync(1);

			// Assert
			Assert.IsNotNull(viewModel.List);
		}

		[TestMethod]
		public async Task DeleteModelAsync_AfterCompletion_CurrentViewIsList()
		{
			// Arrange
			ManageProjectsViewModel viewModel = CreateViewModel();
			ManageProjectsViewModel.AvailableViews expected = ManageProjectsViewModel.AvailableViews.List;
			ManageProjectsViewModel.AvailableViews actual;

			// Act
			await viewModel.DeleteModelAsync(1);
			actual = viewModel.CurrentView;

			// Assert
			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region Private Helper Methods

		/// <summary>
		/// Create a new ViewModel for use in testing
		/// </summary>
		private ManageProjectsViewModel CreateViewModel()
		{
			ManageProjectsViewModel viewModel;

			viewModel = _testDIContext.GetRequiredService<ManageProjectsViewModel>();

			return viewModel;
		}

		/// <summary>
		/// Assign valid data to the properties of the model in order to enable saving it
		/// </summary>
		/// <param name="viewModel">The ViewModel whose model we are to modify</param>
		private void AssignValidModelData(ManageProjectsViewModel viewModel)
		{
			// TODO: Set valid data against the Model object's properties to enable saving of it
			viewModel.Model.Name = "Fred Smith";
			viewModel.Model.CreatedBy = "Fred Smith";
            viewModel.Model.UpdatedBy = "Fred Smith";
        }

        #endregion

    }
}