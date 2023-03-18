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

		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
		public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(nameof(Description));
		public static readonly PropertyInfo<DateTime> StartedProperty = RegisterProperty<DateTime>(nameof(Started));
		public static readonly PropertyInfo<DateTime> EndedProperty = RegisterProperty<DateTime>(nameof(Ended));
		public static readonly PropertyInfo<DateTime> CreatedAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> CreatedByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> UpdatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> UpdatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		public static readonly PropertyInfo<ProjectResources> ProjectResourcesProperty = RegisterProperty<ProjectResources> (nameof(ProjectResources));
		
		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(IdProperty); }
		}

		[Required]
		[MaxLength(50)]
		//[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string Name
		{
			get { return GetProperty(NameProperty); }
			set { SetProperty(NameProperty, value); }
		}

		[MaxLength(1000)]
		//[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string Description
		{
			get { return GetProperty(DescriptionProperty); }
			set { SetProperty(DescriptionProperty, value); }
		}

		public DateTime Started
		{
			get { return GetProperty(StartedProperty); }
			set { SetProperty(StartedProperty, value); }
		}

		public DateTime Ended
		{
			get { return GetProperty(EndedProperty); }
			set { SetProperty(EndedProperty, value); }
		}

		[Required]
		public DateTime CreatedAt
		{
			get { return GetProperty(CreatedAtProperty); }
		}

		[Required]
		public string CreatedBy
		{
			get { return GetProperty(CreatedByProperty); }
			set { SetProperty(CreatedByProperty, value); }
		}

		[Required]
		public DateTime UpdatedAt
		{
			get { return GetProperty(UpdatedAtProperty); }
		}

		[Required]
		public string UpdatedBy
		{
			get { return GetProperty(UpdatedByProperty); }
			set { SetProperty(UpdatedByProperty, value); }
		}

		public ProjectResources ProjectResources
		{
			 get { return ReadProperty(ProjectResourcesProperty); }
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

			public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));

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
				get { return ReadProperty(IdProperty); }
				private set { LoadProperty(IdProperty, value); }
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
				LoadProperty(CreatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(UpdatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(CreatedAtProperty, DateTime.Now);
				LoadProperty(UpdatedAtProperty, DateTime.Now);
				LoadProperty(ProjectResourcesProperty, await projectResourcesDataPortal.CreateChildAsync());
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
				LoadProperty(IdProperty, data.Id);
				LoadProperty(NameProperty, data.Name);
				LoadProperty(DescriptionProperty, data.Description);
				LoadProperty(StartedProperty, data.Started);
				LoadProperty(EndedProperty, data.Ended);
				LoadProperty(CreatedAtProperty, data.CreatedAt);
				LoadProperty(CreatedByProperty, data.CreatedBy);
				LoadProperty(UpdatedAtProperty, data.UpdatedAt);
				LoadProperty(UpdatedByProperty, data.UpdatedBy);
				// Complete the load by requesting any children load themselves
				LoadProperty(ProjectResourcesProperty, await projectResourcesDataPortal.FetchChildAsync(data.Id));
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

			data.Id = ReadProperty(IdProperty);
			data.Name = ReadProperty(NameProperty);
			data.Description = ReadProperty(DescriptionProperty);
			data.Started = ReadProperty(StartedProperty);
			data.Ended = ReadProperty(EndedProperty);
			data.CreatedAt = ReadProperty(CreatedAtProperty);
			data.CreatedBy = ReadProperty(CreatedByProperty);
			data.UpdatedAt = ReadProperty(UpdatedAtProperty);
			data.UpdatedBy = ReadProperty(UpdatedByProperty);

			return data;
		}

		private void LoadObjectChanges(ProjectDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(IdProperty, data.Id);
				LoadProperty(CreatedAtProperty, data.CreatedAt);
				LoadProperty(UpdatedAtProperty, data.UpdatedAt);
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