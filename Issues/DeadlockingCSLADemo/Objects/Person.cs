using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
using DotNotStandard.Validation.Core;
using DeadlockingCSLADemo.Objects.DataAccess;
using System.Net.Http;

// Built to be of the EditableRoot stereotype

namespace DeadlockingCSLADemo.Objects
{

	[Serializable]
	public class Person : BusinessBase<Person>, INestedChildParent
	{

		public static readonly PropertyInfo<int> _personIdProperty = RegisterProperty<int>(nameof(PersonId));
		public static readonly PropertyInfo<string> _firstNameProperty = RegisterProperty<string>(nameof(FirstName));
		public static readonly PropertyInfo<string> _lastNameProperty = RegisterProperty<string>(nameof(LastName));
		public static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		public static readonly PropertyInfo<NestedChildren> _nestedChildrenProperty = RegisterProperty<NestedChildren>(nameof(NestedChildren));

		#region Exposed Properties and Methods

		public int PersonId
		{
			get { return GetProperty(_personIdProperty); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string FirstName
		{
			get { return GetProperty(_firstNameProperty); }
			set { SetProperty(_firstNameProperty, value); }
		}

		[Required]
		[MaxLength(100)]
		[CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string LastName
		{
			get { return GetProperty(_lastNameProperty); }
			set { SetProperty(_lastNameProperty, value); }
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

		public NestedChildren NestedChildren
		{
			get { return GetProperty(_nestedChildrenProperty); }
		}

		public int? ParentNestedChildId { get { return null; } }

		#endregion

		#region Factory Methods

		private Person() 
		{
			// Enforce use of factory methods
		}

		/// <summary>
		/// Create a new Person with appropriate default values
		/// </summary>
		/// <returns>A new Person</returns>
		public static async Task<Person> NewPersonAsync()
		{
			return await DataPortal.CreateAsync<Person>();
		}

		/// <summary>
		/// Get an existing Person from the data store
		/// </summary>
		/// <returns>The requested Person</returns>
		public static async Task<Person> GetPersonAsync(int personId)
		{
			return await DataPortal.FetchAsync<Person>(new Criteria(personId));
		}

		/// <summary>
		/// Deletes a single Person from the data store
		/// </summary>
		public static async Task DeletePersonAsync(int personId)
		{
			await DataPortal.DeleteAsync<Person>(new Criteria(personId));
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
			// Define authorisation rules
			Csla.Rules.BusinessRules.AddRule(typeof(Person),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "Users"));
			Csla.Rules.BusinessRules.AddRule(typeof(Person),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "Users"));
			Csla.Rules.BusinessRules.AddRule(typeof(Person),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "Users"));
			Csla.Rules.BusinessRules.AddRule(typeof(Person),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "Users"));
		}

		#endregion

		#region Data Access

		#region Criteria

		[Serializable]
		private class Criteria : CriteriaBase<Criteria>
		{

			private static readonly PropertyInfo<int> _personIdProperty = RegisterProperty<int>(nameof(PersonId));

			public Criteria()
			{
				// Required for deserialization
			}

			public Criteria(int personId)
			{
				PersonId = personId;
			}
		
			public int PersonId
			{
				get { return ReadProperty(_personIdProperty); }
				private set { LoadProperty(_personIdProperty, value); }
			}

		}
   
		#endregion

		[Create]
		[RunLocal]
		private Task DataPortal_CreateAsync()
		{
			using (BypassPropertyChecks)
			{
				// Set default values for any fields that need to be initialised
				LoadProperty(_createdByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_updatedByProperty, Csla.ApplicationContext.User?.Identity?.Name);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
				LoadProperty(_nestedChildrenProperty, DataPortal.CreateChild<NestedChildren>());
			}
			BusinessRules.CheckRules();

			return Task.CompletedTask;
		}

		[Fetch]
		private async Task DataPortal_FetchAsync(Criteria criteria, [Inject] IPersonRepository repository)
		{
			PersonDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			data = await repository.FetchAsync(criteria.PersonId);
			await LoadObjectAsync(data);
		}

		private async Task LoadObjectAsync(PersonDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_personIdProperty, data.PersonId);
				LoadProperty(_firstNameProperty, data.FirstName);
				LoadProperty(_lastNameProperty, data.LastName);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_createdByProperty, data.CreatedBy);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
				LoadProperty(_updatedByProperty, data.UpdatedBy);
				// Complete the load by requesting any children load themselves
				LoadProperty(_nestedChildrenProperty, await DataPortal.FetchChildAsync<NestedChildren>(data.PersonId));
			}

			// Check that the object retrieved from the store meets the latest business rules
			BusinessRules.CheckRules();
		}

		[Insert]
		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted, TransactionScopeAsyncFlowOption.Enabled)]
		private async Task DataPortal_InsertAsync([Inject] IPersonRepository repository)
		{
			PersonDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			data = LoadDTO();
			data = await repository.InsertAsync(data);
			LoadObjectChanges(data);

			// Request that any children insert themselves as well
			FieldManager.UpdateChildren(this);
		}

		[Update]
		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted, TransactionScopeAsyncFlowOption.Enabled)]
		private async Task DataPortal_UpdateAsync([Inject] IPersonRepository repository)
		{
			PersonDTO data;

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

		private PersonDTO LoadDTO()
		{
			PersonDTO data = new PersonDTO();

			data.PersonId = ReadProperty(_personIdProperty);
			data.FirstName = ReadProperty(_firstNameProperty);
			data.LastName = ReadProperty(_lastNameProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(PersonDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_personIdProperty, data.PersonId);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[Delete]
		private async Task DataPortal_DeleteAsync(Criteria criteria, [Inject] IPersonRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await repository.DeleteAsync(criteria.PersonId);
		}

		#endregion

	}
}