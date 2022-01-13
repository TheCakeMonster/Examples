using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using VehicleTracker.Objects.DataAccess;

// Generated from the built-in Scriban CSLA ReadOnlyRootList template

namespace VehicleTracker.Objects
{
	[Serializable]
	public class VehicleList : ReadOnlyListBase<VehicleList, VehicleInfo>
	{

		#region Instantiation

		public VehicleList() 
		{
			// Has to be public for CSLA to work, but shouldn't ever be used!
		}

		#endregion

		#region Authorisation 

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void AddObjectAuthorizationRules()
		{
			// TODO: Work out fake authentication to get authorisation to work
			//// Define authorisation rules
			//Csla.Rules.BusinessRules.AddRule(typeof(VehicleList),
			//	new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "Users"));
		}

		#endregion

		#region Data Access

		#region Criteria

		//[Serializable]
		//private class Criteria : CriteriaBase<Criteria>
		//{
		//
		//	private static readonly PropertyInfo<int> _parentIdProperty = RegisterProperty<int>(nameof(ParentId));
		//
		//	public Criteria()
		//	{
		//		// Required for deserialization
		//	}
		//
		//	public Criteria(int parentId)
		//	{
		//		ParentId = parentId;
		//	}
		//
		//	public int ParentId
		//	{
		//		get { return ReadProperty(_parentIdProperty); }
		//		private set { LoadProperty(_parentIdProperty, value); }
		//	}
		//}

		#endregion

		[Fetch]
		private async Task DataPortal_FetchAsync([Inject]IChildDataPortal<VehicleInfo> childDataPortal, [Inject]IVehicleRepository repository)
		{
			IList<VehicleDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchListAsync();
			await LoadObjectsAsync(items, childDataPortal);
		}

		private async Task LoadObjectsAsync(IList<VehicleDTO> items, IChildDataPortal<VehicleInfo> childDataPortal)
		{
			bool raiseEvents;

			raiseEvents = RaiseListChangedEvents;
			RaiseListChangedEvents = false;
			IsReadOnly = false;

			foreach (var item in items)
			{
				Add(await childDataPortal.FetchChildAsync(item));
			}

			IsReadOnly = true;
			RaiseListChangedEvents = raiseEvents;
		}

		#endregion

	}
}
