using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
using DeadlockingCSLADemo.Objects.DataAccess;
using DotNotStandard.Validation.Core;

// Built to be of the CSLA EditableChild stereotype

namespace DeadlockingCSLADemo.Objects
{
	[Serializable]
	public class NestedChild : BusinessBase<NestedChild>, INestedChildParent
	{

		public static readonly PropertyInfo<int> _nestedChildIdProperty = RegisterProperty<int>(nameof(NestedChildId));
		public static readonly PropertyInfo<int> _personIdProperty = RegisterProperty<int>(nameof(PersonId));
		public static readonly PropertyInfo<int?> _parentNestedChildIdProperty = RegisterProperty<int?>(nameof(ParentNestedChildId));
		public static readonly PropertyInfo<string> _childNameProperty = RegisterProperty<string>(nameof(ChildName));
		public static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		public static readonly PropertyInfo<NestedChildren> _nestedChildrenProperty = RegisterProperty<NestedChildren>(nameof(NestedChildren));

		#region Exposed Properties and Methods

		public int NestedChildId
		{
			get { return GetProperty(_nestedChildIdProperty); }
		}

		public int PersonId
		{
			get { return GetProperty(_personIdProperty); }
			private set { SetProperty(_personIdProperty, value); }
		}

		public int? ParentNestedChildId
		{
			get { return GetProperty(_parentNestedChildIdProperty); }
			private set { SetProperty(_parentNestedChildIdProperty, value); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string ChildName
		{
			get { return GetProperty(_childNameProperty); }
			set { SetProperty(_childNameProperty, value); }
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

		public NestedChildren NestedChildren 
		{ 
			get { return GetProperty(_nestedChildrenProperty); } 
		}

		#endregion

		#region Factory Methods

		private NestedChild() 
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
		private async Task DataPortal_CreateAsync()
		{
			using (BypassPropertyChecks)
			{
				// Set default values for any fields that need to be initialised
				LoadProperty(_createdByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_updatedByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
				LoadProperty(_nestedChildrenProperty, await DataPortal.CreateAsync<NestedChildren>());
			}
			BusinessRules.CheckRules();
		}

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(NestedChildDTO data, [Inject]INestedChildRepository repository)
		{
			await LoadObjectAsync(data);
		}

		private async Task LoadObjectAsync(NestedChildDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_nestedChildIdProperty, data.NestedChildId);
				LoadProperty(_personIdProperty, data.PersonId);
				LoadProperty(_parentNestedChildIdProperty, data.ParentNestedChildId);
				LoadProperty(_childNameProperty, data.ChildName);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_createdByProperty, data.CreatedBy);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
				LoadProperty(_updatedByProperty, data.UpdatedBy);
				// Complete the load by requesting any children load themselves
				LoadProperty(_nestedChildrenProperty, await DataPortal.FetchChildAsync<NestedChildren>(data));
			}

			// Check that the object retrieved from the store meets the latest business rules
			BusinessRules.CheckRules();
		}

		[InsertChild]
		private async Task DataPortal_InsertAsync(Person parent, [Inject] INestedChildRepository repository)
		{
			await DoInsertAsync(parent, repository);
		}

		[InsertChild]
		private async Task DataPortal_InsertAsync(NestedChild parent, [Inject] INestedChildRepository repository)
		{
			await DoInsertAsync(parent, repository);
		}

		private async Task DoInsertAsync(INestedChildParent parent, [Inject]INestedChildRepository repository)
		{
			NestedChildDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			// Write parent's unique identifiers into the child
			LoadProperty(_personIdProperty, parent.PersonId);
			LoadProperty(_nestedChildIdProperty, parent.ParentNestedChildId);
			data = LoadDTO();
			data = await repository.InsertAsync(data);
			LoadObjectChanges(data);

			// Request that children insert themselves as well
			FieldManager.UpdateChildren(this);
		}

		[UpdateChild]
		private async Task DataPortal_UpdateAsync(Person parent, [Inject] INestedChildRepository repository)
		{
			await DoUpdateAsync(parent, repository);
		}

		[UpdateChild]
		private async Task DataPortal_UpdateAsync(NestedChild parent, [Inject] INestedChildRepository repository)
		{
			await DoUpdateAsync(parent, repository);
		}

		private async Task DoUpdateAsync(INestedChildParent parent, INestedChildRepository repository)
		{
			NestedChildDTO data;

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

		private NestedChildDTO LoadDTO()
		{
			NestedChildDTO data = new NestedChildDTO();

			data.NestedChildId = ReadProperty(_nestedChildIdProperty);
			data.PersonId = ReadProperty(_personIdProperty);
			data.ChildName = ReadProperty(_childNameProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(NestedChildDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_nestedChildIdProperty, data.NestedChildId);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[DeleteSelfChild]
		private async Task DataPortal_DeleteSelfAsync([Inject]INestedChildRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			int customPropertyId = ReadProperty(_nestedChildIdProperty);
			await repository.DeleteAsync(customPropertyId);
		}

		#endregion

	}
}