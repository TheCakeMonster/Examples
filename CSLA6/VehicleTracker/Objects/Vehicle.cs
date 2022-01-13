using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Transactions;
using Csla;
//using DotNotStandard.Validation.Core;
using VehicleTracker.Objects.DataAccess;

// Generated from the built-in Scriban CSLA EditableRoot template

namespace VehicleTracker.Objects
{

	[Serializable]
	public class Vehicle : BusinessBase<Vehicle>
	{

		private static readonly PropertyInfo<int> _vehicleIdProperty = RegisterProperty<int>(nameof(VehicleId));
		private static readonly PropertyInfo<string> _nickNameProperty = RegisterProperty<string>(nameof(NickName));
		private static readonly PropertyInfo<string> _keyReferenceProperty = RegisterProperty<string>(nameof(KeyReference));
		private static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		private static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		private static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		private static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		
		#region Exposed Properties and Methods

		public int VehicleId
		{
			get { return GetProperty(_vehicleIdProperty); }
		}

		[Required]
		[MaxLength(100)]
		// [CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string NickName
		{
			get { return GetProperty(_nickNameProperty); }
			set { SetProperty(_nickNameProperty, value); }
		}

		[Required]
		[MaxLength(15)]
		// [CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string KeyReference
		{
			get { return GetProperty(_keyReferenceProperty); }
			set { SetProperty(_keyReferenceProperty, value); }
		}

		[Required]
		public DateTime CreatedAt
		{
			get { return GetProperty(_createdAtProperty); }
		}

		[Required]
		[MaxLength(100)]
		// [CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
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
		// [CharacterSet(BuiltInRules.CharacterSet.LatinAlphanumeric)]
		public string UpdatedBy
		{
			get { return GetProperty(_updatedByProperty); }
			set { SetProperty(_updatedByProperty, value); }
		}

		#endregion

		#region Instantiation

		public Vehicle() 
		{
			// Constructor is required for CSLA to work, but should never be used!
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
			// TODO: Work out fake authentication to get authorisation to work
			//// Define authorisation rules
			//Csla.Rules.BusinessRules.AddRule(typeof(Vehicle),
			//	new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "Users"));
			//Csla.Rules.BusinessRules.AddRule(typeof(Vehicle),
			//	new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "Users"));
			//Csla.Rules.BusinessRules.AddRule(typeof(Vehicle),
			//	new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "Users"));
			//Csla.Rules.BusinessRules.AddRule(typeof(Vehicle),
			//	new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "Users"));
		}

		#endregion

		#region Data Access

		#region Criteria

		[Serializable]
		internal class Criteria : CriteriaBase<Criteria>
		{

			private static readonly PropertyInfo<int> _vehicleIdProperty = RegisterProperty<int>(nameof(VehicleId));

			public Criteria()
			{
				// Required for deserialization
			}

			public Criteria(int vehicleId)
			{
				VehicleId = vehicleId;
			}
		
			public int VehicleId
			{
				get { return ReadProperty(_vehicleIdProperty); }
				private set { LoadProperty(_vehicleIdProperty, value); }
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
				LoadProperty(_createdByProperty, ApplicationContext.Principal?.Identity?.Name);
				LoadProperty(_updatedByProperty, ApplicationContext.Principal?.Identity?.Name);
				LoadProperty(_createdAtProperty, DateTime.Now);
				LoadProperty(_updatedAtProperty, DateTime.Now);
			}
			BusinessRules.CheckRules();

			return Task.CompletedTask;
		}

		[Fetch]
		private async Task DataPortal_FetchAsync(Criteria criteria, [Inject]IVehicleRepository repository)
		{
			VehicleDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			data = await repository.FetchAsync(criteria.VehicleId);
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(VehicleDTO data)
		{
			// Load the object from the DTO
			using (BypassPropertyChecks)
			{
				LoadProperty(_vehicleIdProperty, data.VehicleId);
				LoadProperty(_nickNameProperty, data.NickName);
				LoadProperty(_keyReferenceProperty, data.KeyReference);
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

		[Insert]
		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted, TransactionScopeAsyncFlowOption.Enabled)]
		private async Task DataPortal_InsertAsync([Inject]IVehicleRepository repository)
		{
			VehicleDTO data;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			data = LoadDTO();
			data = await repository.InsertAsync(data);
			LoadObjectChanges(data);

			// Request that any children insert themselves as well
			FieldManager.UpdateChildren(this);
		}

		[Update]
		[Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted, TransactionScopeAsyncFlowOption.Enabled)]
		private async Task DataPortal_UpdateAsync([Inject]IVehicleRepository repository)
		{
			VehicleDTO data;

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

		private VehicleDTO LoadDTO()
		{
			VehicleDTO data = new VehicleDTO();

			data.VehicleId = ReadProperty(_vehicleIdProperty);
			data.NickName = ReadProperty(_nickNameProperty);
			data.KeyReference = ReadProperty(_keyReferenceProperty);
			data.CreatedAt = ReadProperty(_createdAtProperty);
			data.CreatedBy = ReadProperty(_createdByProperty);
			data.UpdatedAt = ReadProperty(_updatedAtProperty);
			data.UpdatedBy = ReadProperty(_updatedByProperty);

			return data;
		}

		private void LoadObjectChanges(VehicleDTO data)
		{
			using (BypassPropertyChecks)
			{
				LoadProperty(_vehicleIdProperty, data.VehicleId);
				LoadProperty(_createdAtProperty, data.CreatedAt);
				LoadProperty(_updatedAtProperty, data.UpdatedAt);
			}
		}

		[Delete]
		private async Task DataPortal_DeleteAsync(Criteria criteria, [Inject]IVehicleRepository repository)
		{
			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await repository.DeleteAsync(criteria.VehicleId);
		}

		#endregion

	}
}