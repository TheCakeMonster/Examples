using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
using DotNotStandard.DataAccess.Core;
using DotNotStandard.Validation.Core;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban CSLA EditableChild template

namespace ProjectTracker.Objects
{
	[Serializable]
	public class ProjectResourceEdit : BusinessBase<ProjectResourceEdit>
	{

		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
		public static readonly PropertyInfo<int> ProjectIdProperty = RegisterProperty<int>(nameof(ProjectId));
		public static readonly PropertyInfo<int> ResourceIdProperty = RegisterProperty<int>(nameof(ResourceId));
		public static readonly PropertyInfo<int> RoleIdProperty = RegisterProperty<int>(nameof(RoleId));
		public static readonly PropertyInfo<DateTime> AssignedProperty = RegisterProperty<DateTime>(nameof(Assigned));
		public static readonly PropertyInfo<DateTime> CreatedAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> CreatedByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> UpdatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> UpdatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));

		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(IdProperty); }
		}

		public int ProjectId
		{
			get { return GetProperty(ProjectIdProperty); }
			set { SetProperty(ProjectIdProperty, value); }
		}

		public int ResourceId
		{
			get { return GetProperty(ResourceIdProperty); }
			set { SetProperty(ResourceIdProperty, value); }
		}

		public int RoleId
		{
			get { return GetProperty(RoleIdProperty); }
			set { SetProperty(RoleIdProperty, value); }
		}

		[Required]
		public DateTime Assigned
		{
			get { return GetProperty(AssignedProperty); }
			set { SetProperty(AssignedProperty, value); }
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


		#endregion

		#region Factory Methods

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
			// TODO: Add any per-property authorisation rules required
		}

		#endregion

		#region Data Access

		[CreateChild]
		private Task DataPortal_CreateAsync()
		{
			using (BypassPropertyChecks)
			{
				// TODO: Set default values for any fields that need to be initialised
				LoadProperty(CreatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(UpdatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(CreatedAtProperty, DateTime.Now);
				LoadProperty(UpdatedAtProperty, DateTime.Now);
			}
			BusinessRules.CheckRules();

			return Task.CompletedTask;
		}

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(AssignmentDTO data)
		{
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(AssignmentDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(IdProperty, data.Id);
				LoadProperty(ProjectIdProperty, data.ProjectId);
				LoadProperty(ResourceIdProperty, data.ResourceId);
				LoadProperty(RoleIdProperty, data.RoleId);
				LoadProperty(AssignedProperty, data.Assigned);
				LoadProperty(CreatedAtProperty, data.CreatedAt);
				LoadProperty(CreatedByProperty, data.CreatedBy);
				LoadProperty(UpdatedAtProperty, data.UpdatedAt);
				LoadProperty(UpdatedByProperty, data.UpdatedBy);
				// Complete the load by requesting any children load themselves
			}

			// Check that the object retrieved from the store meets the latest business rules
			BusinessRules.CheckRules();

			return Task.CompletedTask;
		}

		[InsertChild]
		private async Task DataPortal_InsertAsync(ProjectEdit parent, [Inject]IAssignmentRepository repository)
		{
			AssignmentDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			// Write parent's unique identifiers into the child
			LoadProperty(ProjectIdProperty, parent.Id);
			data = LoadDTO();
			data = await repository.InsertAsync(data);
			LoadObjectChanges(data);

			// Request that children insert themselves as well
			await FieldManager.UpdateChildrenAsync(this);
		}

		[UpdateChild]
		private async Task DataPortal_UpdateAsync(ProjectEdit parent, [Inject]IAssignmentRepository repository)
		{
			AssignmentDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			if (IsSelfDirty)
			{
				// Write parent's unique identifiers into the child, if they are editable
				data = LoadDTO();
				data = await repository.UpdateAsync(data);
				LoadObjectChanges(data);
			}

			// Request that any children update themselves as well
			await FieldManager.UpdateChildrenAsync(this);
		}

		private AssignmentDTO LoadDTO()
		{
			AssignmentDTO data = new AssignmentDTO();

			data.Id = ReadProperty(IdProperty);
			data.ProjectId = ReadProperty(ProjectIdProperty);
			data.ResourceId = ReadProperty(ResourceIdProperty);
			data.RoleId = ReadProperty(RoleIdProperty);
			data.Assigned = ReadProperty(AssignedProperty);
			data.CreatedAt = ReadProperty(CreatedAtProperty);
			data.CreatedBy = ReadProperty(CreatedByProperty);
			data.UpdatedAt = ReadProperty(UpdatedAtProperty);
			data.UpdatedBy = ReadProperty(UpdatedByProperty);

			return data;
		}

		private void LoadObjectChanges(AssignmentDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(IdProperty, data.Id);
				LoadProperty(CreatedAtProperty, data.CreatedAt);
				LoadProperty(UpdatedAtProperty, data.UpdatedAt);
			}
		}

		[DeleteSelfChild]
		private async Task DataPortal_DeleteSelfAsync(ProjectEdit parent, [Inject]IAssignmentRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			int id = ReadProperty(IdProperty);
			await repository.DeleteAsync(id);
		}

		#endregion

	}
}