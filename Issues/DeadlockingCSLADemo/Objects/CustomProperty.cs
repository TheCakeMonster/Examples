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
	public class CustomProperty : BusinessBase<CustomProperty>
	{

		public static readonly PropertyInfo<int> _customPropertyIdProperty = RegisterProperty<int>(nameof(CustomPropertyId));
		public static readonly PropertyInfo<int> _personIdProperty = RegisterProperty<int>(nameof(PersonId));
		public static readonly PropertyInfo<string> _propertyNameProperty = RegisterProperty<string>(nameof(PropertyName));
		public static readonly PropertyInfo<string> _propertyValueProperty = RegisterProperty<string>(nameof(PropertyValue));
		public static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));

		#region Exposed Properties and Methods

		public int CustomPropertyId
		{
			get { return GetProperty(_customPropertyIdProperty); }
		}

		public int PersonId
		{
			get { return GetProperty(_personIdProperty); }
			private set { SetProperty(_personIdProperty, value); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string PropertyName
		{
			get { return GetProperty(_propertyNameProperty); }
			set { SetProperty(_propertyNameProperty, value); }
		}

		[Required]
		[MaxLength(1000)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string PropertyValue
		{
			get { return GetProperty(_propertyValueProperty); }
			set { SetProperty(_propertyValueProperty, value); }
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

		private CustomProperty() 
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
		private async Task DataPortal_FetchChildAsync(CustomPropertyDTO data, [Inject]ICustomPropertyRepository repository)
		{
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(CustomPropertyDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_customPropertyIdProperty, data.CustomPropertyId);
				LoadProperty(_personIdProperty, data.PersonId);
				LoadProperty(_propertyNameProperty, data.PropertyName);
				LoadProperty(_propertyValueProperty, data.PropertyValue);
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
		private async Task DataPortal_InsertAsync(Person parent, [Inject]ICustomPropertyRepository repository)
		{
			CustomPropertyDTO data;

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
		private async Task DataPortal_UpdateAsync(Person parent, [Inject]ICustomPropertyRepository repository)
		{
			CustomPropertyDTO data;

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

		private CustomPropertyDTO LoadDTO()
		{
			CustomPropertyDTO data = new CustomPropertyDTO();

			data.CustomPropertyId = ReadProperty(_customPropertyIdProperty);
			data.PersonId = ReadProperty(_personIdProperty);
			data.PropertyName = ReadProperty(_propertyNameProperty);
			data.PropertyValue = ReadProperty(_propertyValueProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(CustomPropertyDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_customPropertyIdProperty, data.CustomPropertyId);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[DeleteSelfChild]
		private async Task DataPortal_DeleteSelfAsync([Inject]ICustomPropertyRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			int customPropertyId = ReadProperty(_customPropertyIdProperty);
			await repository.DeleteAsync(customPropertyId);
		}

		#endregion

	}
}