using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
using DotNotStandard.Validation.Core;
using DeadlockingCSLADemo.Objects.DataAccess;

// Built to be of the CSLA EditableChild stereotype

namespace DeadlockingCSLADemo.Objects
{
	[Serializable]
	public class EmploymentHistory : BusinessBase<EmploymentHistory>
	{

		public static readonly PropertyInfo<int> _employmentHistoryIdProperty = RegisterProperty<int>(nameof(EmploymentHistoryId));
		public static readonly PropertyInfo<string> _employerNameProperty = RegisterProperty<string>(nameof(EmployerName));
		public static readonly PropertyInfo<DateTime> _joinedOnProperty = RegisterProperty<DateTime>(nameof(JoinedOn));
		public static readonly PropertyInfo<DateTime> _departedOnProperty = RegisterProperty<DateTime>(nameof(DepartedOn));
		public static readonly PropertyInfo<int> _personIdProperty = RegisterProperty<int>(nameof(PersonId));
		public static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));

		#region Exposed Properties and Methods

		public int EmploymentHistoryId
		{
			get { return GetProperty(_employmentHistoryIdProperty); }
		}

		public int PersonId
		{
			get { return GetProperty(_personIdProperty); }
			private set { SetProperty(_personIdProperty, value); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string EmployerName
		{
			get { return GetProperty(_employerNameProperty); }
			set { SetProperty(_employerNameProperty, value); }
		}

		[Required]
		public DateTime JoinedOn
		{
			get { return GetProperty(_joinedOnProperty); }
			set { SetProperty(_joinedOnProperty, value); }
		}

		public DateTime DepartedOn
		{
			get { return GetProperty(_departedOnProperty); }
			set { SetProperty(_departedOnProperty, value); }
		}

		[Required]
		public DateTime CreatedAt
		{
			get { return GetProperty(_createdAtProperty); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
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
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string UpdatedBy
		{
			get { return GetProperty(_updatedByProperty); }
			set { SetProperty(_updatedByProperty, value); }
		}

		#endregion

		#region Factory Methods

		private EmploymentHistory() 
		{
			// Enforce use of factory methods
		}

		#endregion

		#region Validation

		protected override void AddBusinessRules()
		{
			base.AddBusinessRules();

			// Add any complex business rules that cannot be achieved through attributes
		}
		
		#endregion

		#region Authorisation 

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void AddObjectAuthorizationRules()
		{
			// Add any per-property authorisation rules required
		}

		#endregion

		#region Data Access

		[CreateChild]
		private Task DataPortal_CreateAsync()
		{
			using (BypassPropertyChecks)
			{
				// Set default values for any fields that need to be initialised
				LoadProperty(_createdByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_updatedByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
			}
			BusinessRules.CheckRules();

			return Task.CompletedTask;
		}

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(EmploymentHistoryDTO data, [Inject]IEmploymentHistoryRepository repository)
		{
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(EmploymentHistoryDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_employmentHistoryIdProperty, data.EmploymentHistoryId);
				LoadProperty(_employerNameProperty, data.EmployerName);
				LoadProperty(_joinedOnProperty, data.JoinedOn);
				LoadProperty(_departedOnProperty, data.DepartedOn);
				LoadProperty(_personIdProperty, data.PersonId);
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
		private async Task DataPortal_InsertAsync(Person parent, [Inject]IEmploymentHistoryRepository repository)
		{
			EmploymentHistoryDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			// Write parent's unique identifiers into the child
			LoadProperty(_personIdProperty, parent.PersonId);
			data = LoadDTO();
			data = await repository.InsertAsync(data);
			LoadObjectChanges(data);

			// Request that children insert themselves as well
			FieldManager.UpdateChildren(this);
		}

		[UpdateChild]
		private async Task DataPortal_UpdateAsync(Person parent, [Inject]IEmploymentHistoryRepository repository)
		{
			EmploymentHistoryDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			if (IsSelfDirty)
			{
				data = LoadDTO();
				data = await repository.UpdateAsync(data);
				LoadObjectChanges(data);
			}

			// Request that any children update themselves as well
			FieldManager.UpdateChildren(this);
		}

		private EmploymentHistoryDTO LoadDTO()
		{
			EmploymentHistoryDTO data = new EmploymentHistoryDTO();

			data.EmploymentHistoryId = ReadProperty(_employmentHistoryIdProperty);
			data.EmployerName = ReadProperty(_employerNameProperty);
			data.JoinedOn = ReadProperty(_joinedOnProperty);
			data.DepartedOn = ReadProperty(_departedOnProperty);
			data.PersonId = ReadProperty(_personIdProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(EmploymentHistoryDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_employmentHistoryIdProperty, data.EmploymentHistoryId);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[DeleteSelfChild]
		private async Task DataPortal_DeleteSelfAsync([Inject]IEmploymentHistoryRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			int employmentHistoryId = ReadProperty(_employmentHistoryIdProperty);
			await repository.DeleteAsync(employmentHistoryId);
		}

		#endregion

	}
}