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

		private static readonly PropertyInfo<int> _idProperty = RegisterProperty<int>(nameof(Id));
		private static readonly PropertyInfo<int> _projectIdProperty = RegisterProperty<int>(nameof(ProjectId));
		private static readonly PropertyInfo<int> _resourceIdProperty = RegisterProperty<int>(nameof(ResourceId));
		private static readonly PropertyInfo<int> _roleIdProperty = RegisterProperty<int>(nameof(RoleId));
		private static readonly PropertyInfo<DateTime> _assignedProperty = RegisterProperty<DateTime>(nameof(Assigned));
		private static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		private static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		private static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		private static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));

		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(_idProperty); }
		}

		public int ProjectId
		{
			get { return GetProperty(_projectIdProperty); }
			set { SetProperty(_projectIdProperty, value); }
		}

		public int ResourceId
		{
			get { return GetProperty(_resourceIdProperty); }
			set { SetProperty(_resourceIdProperty, value); }
		}

		public int RoleId
		{
			get { return GetProperty(_roleIdProperty); }
			set { SetProperty(_roleIdProperty, value); }
		}

		[Required]
		public DateTime Assigned
		{
			get { return GetProperty(_assignedProperty); }
			set { SetProperty(_assignedProperty, value); }
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
				LoadProperty(_createdByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(_updatedByProperty, ApplicationContext.User?.Identity?.Name ?? string.Empty);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
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
				LoadProperty(_idProperty, data.Id);
				LoadProperty(_projectIdProperty, data.ProjectId);
				LoadProperty(_resourceIdProperty, data.ResourceId);
				LoadProperty(_roleIdProperty, data.RoleId);
				LoadProperty(_assignedProperty, data.Assigned);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_createdByProperty, data.CreatedBy);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
				LoadProperty(_updatedByProperty, data.UpdatedBy);
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
			LoadProperty(_projectIdProperty, parent.Id);
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

			data.Id = ReadProperty(_idProperty);
			data.ProjectId = ReadProperty(_projectIdProperty);
			data.ResourceId = ReadProperty(_resourceIdProperty);
			data.RoleId = ReadProperty(_roleIdProperty);
			data.Assigned = ReadProperty(_assignedProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(AssignmentDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_idProperty, data.Id);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[DeleteSelfChild]
		private async Task DataPortal_DeleteSelfAsync(ProjectEdit parent, [Inject]IAssignmentRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			int id = ReadProperty(_idProperty);
			await repository.DeleteAsync(id);
		}

		#endregion

	}
}