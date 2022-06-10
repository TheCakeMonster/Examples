using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
using DotNotStandard.DataAccess.Core;
using DotNotStandard.Validation.Core;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban CSLA EditableRoot template

namespace ProjectTracker.Objects
{

	[Serializable]
	public class ProjectEdit : BusinessBase<ProjectEdit>
	{

		private static readonly PropertyInfo<int> _idProperty = RegisterProperty<int>(nameof(Id));
		private static readonly PropertyInfo<string> _nameProperty = RegisterProperty<string>(nameof(Name));
		private static readonly PropertyInfo<string> _descriptionProperty = RegisterProperty<string>(nameof(Description));
		private static readonly PropertyInfo<DateTime> _startedProperty = RegisterProperty<DateTime>(nameof(Started));
		private static readonly PropertyInfo<DateTime> _endedProperty = RegisterProperty<DateTime>(nameof(Ended));
		private static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		private static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		private static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		private static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		private static readonly PropertyInfo<ProjectResources> _projectResourcesProperty = RegisterProperty<ProjectResources> (nameof(ProjectResources));
		
		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(_idProperty); }
		}

		[Required]
		[MaxLength(50)]
		//[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string Name
		{
			get { return GetProperty(_nameProperty); }
			set { SetProperty(_nameProperty, value); }
		}

		[MaxLength(1000)]
		//[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string Description
		{
			get { return GetProperty(_descriptionProperty); }
			set { SetProperty(_descriptionProperty, value); }
		}

		public DateTime Started
		{
			get { return GetProperty(_startedProperty); }
			set { SetProperty(_startedProperty, value); }
		}

		public DateTime Ended
		{
			get { return GetProperty(_endedProperty); }
			set { SetProperty(_endedProperty, value); }
		}

		[Required]
		public DateTime CreatedAt
		{
			get { return GetProperty(_createdAtProperty); }
		}

		[Required]
		public string CreatedBy
		{
			get { return GetProperty(_createdByProperty); }
			set { SetProperty(_createdByProperty, value); }
		}

		[Required]
		public DateTime UpdatedAt
		{
			get { return GetProperty(_updatedAtProperty); }
		}

		[Required]
		public string UpdatedBy
		{
			get { return GetProperty(_updatedByProperty); }
			set { SetProperty(_updatedByProperty, value); }
		}

		public ProjectResources ProjectResources
		{
			 get { return ReadProperty(_projectResourcesProperty); }
		}

		#endregion

		#region Factory Methods

		/// <summary>
		/// Factory class for interaction with the parent type
		/// </summary>
		public class Factory
		{
			private IDataPortal<ProjectEdit> _dataPortal;

			public Factory(IDataPortal<ProjectEdit> dataPortal)
			{
				_dataPortal = dataPortal;
			}

			/// <summary>
			/// Create a new ProjectEdit with appropriate default values
			/// </summary>
			/// <returns>A new ProjectEdit</returns>
			public async Task<ProjectEdit> NewProjectEditAsync()
			{
				return await _dataPortal.CreateAsync();
			}

			/// <summary>
			/// Get an existing ProjectEdit from the data store
			/// </summary>
			/// <returns>The requested ProjectEdit</returns>
			public async Task<ProjectEdit> GetProjectEditAsync(int id)
			{
				return await _dataPortal.FetchAsync(new Criteria(id));
			}

			/// <summary>
			/// Deletes a single ProjectEdit from the data store
			/// </summary>
			public async Task DeleteProjectEditAsync(int id)
			{
				await _dataPortal.DeleteAsync(new Criteria(id));
			}
		}

		#endregion

		#region Validation

		protected override void AddBusinessRules()
		{
			base.AddBusinessRules();

			// TODO: Add any complex business rules that cannot be achieved through attributes
		}

		#endregion

		#region Authorisation 

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void AddObjectAuthorizationRules()
		{
			// TODO: Define authorisation rules
			Csla.Rules.BusinessRules.AddRule(typeof(ProjectEdit),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "ProjectManager", "Administrator"));
			Csla.Rules.BusinessRules.AddRule(typeof(ProjectEdit),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "ProjectManager"));
			Csla.Rules.BusinessRules.AddRule(typeof(ProjectEdit),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "ProjectManager"));
			Csla.Rules.BusinessRules.AddRule(typeof(ProjectEdit),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "Administrator"));
		}

		#endregion

		#region Data Access

		#region Criteria

		[Serializable]
		private class Criteria : CriteriaBase<Criteria>
		{

			private static readonly PropertyInfo<int> _idProperty = RegisterProperty<int>(nameof(Id));

			public Criteria()
			{
				// Required for deserialization
			}

			public Criteria(int id)
			{
				Id = id;
			}
		
			public int Id
			{
				get { return ReadProperty(_idProperty); }
				private set { LoadProperty(_idProperty, value); }
			}

		}
   
		#endregion

		[Create]
		[RunLocal]
		private async Task DataPortal_CreateAsync([Inject] IChildDataPortal<ProjectResources> projectResourcesDataPortal)
		{
			using (BypassPropertyChecks)
			{
				// TODO: Set default values for any fields that need to be initialised
				LoadProperty(_createdByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(_updatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
				LoadProperty(_projectResourcesProperty, await projectResourcesDataPortal.CreateChildAsync());
			}
			BusinessRules.CheckRules();

		}

		[Fetch]
		private async Task DataPortal_FetchAsync(Criteria criteria, [Inject] IConnectionManager connectionManager, [Inject] IProjectRepository repository, [Inject] IChildDataPortal<ProjectResources> projectResourcesDataPortal)
		{
			ProjectDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await using (connectionManager.StartTransaction())
            {
				data = await repository.FetchAsync(criteria.Id);
				await LoadObjectAsync(data, projectResourcesDataPortal);

				// Commit the completed work
				await connectionManager.CommitTransactionAsync();
            }
		}

		private async Task LoadObjectAsync(ProjectDTO data, [Inject] IChildDataPortal<ProjectResources> projectResourcesDataPortal)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_idProperty, data.Id);
				LoadProperty(_nameProperty, data.Name);
				LoadProperty(_descriptionProperty, data.Description);
				LoadProperty(_startedProperty, data.Started);
				LoadProperty(_endedProperty, data.Ended);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_createdByProperty, data.CreatedBy);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
				LoadProperty(_updatedByProperty, data.UpdatedBy);
				// Complete the load by requesting any children load themselves
				LoadProperty(_projectResourcesProperty, await projectResourcesDataPortal.FetchChildAsync(data.Id));
			}

			// Check that the object retrieved from the store meets the latest business rules
			BusinessRules.CheckRules();

		}

		[Insert]
		private async Task DataPortal_InsertAsync([Inject] IConnectionManager connectionManager, [Inject]IProjectRepository repository)
		{
			ProjectDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			data = LoadDTO();
			await using (connectionManager.StartTransaction())
            {
				data = await repository.InsertAsync(data);
				LoadObjectChanges(data);

				// Request that any children insert themselves as well
				await FieldManager.UpdateChildrenAsync(this);

				// Commit the completed work
				await connectionManager.CommitTransactionAsync();
            }
		}

		[Update]
		private async Task DataPortal_UpdateAsync([Inject] IConnectionManager connectionManager, [Inject]IProjectRepository repository)
		{
			ProjectDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await using (connectionManager.StartTransaction())
            {
				if (IsSelfDirty)
				{
					data = LoadDTO();
					data = await repository.UpdateAsync(data);
					LoadObjectChanges(data);
				}

				// Request that any children update themselves as well
				await FieldManager.UpdateChildrenAsync(this);

				// Commit the completed work
				await connectionManager.CommitTransactionAsync();
            }
		}

		private ProjectDTO LoadDTO()
		{
			ProjectDTO data = new ProjectDTO();

			data.Id = ReadProperty(_idProperty);
			data.Name = ReadProperty(_nameProperty);
			data.Description = ReadProperty(_descriptionProperty);
			data.Started = ReadProperty(_startedProperty);
			data.Ended = ReadProperty(_endedProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(ProjectDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_idProperty, data.Id);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[Delete]
		private async Task DataPortal_DeleteAsync(Criteria criteria, [Inject] IConnectionManager connectionManager, [Inject]IProjectRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await using (connectionManager.StartTransaction())
            {
				await repository.DeleteAsync(criteria.Id);

				// Commit the completed work
				await connectionManager.CommitTransactionAsync();
            }
		}

		#endregion

	}
}