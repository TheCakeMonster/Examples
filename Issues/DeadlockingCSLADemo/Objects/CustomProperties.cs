using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using DeadlockingCSLADemo.Objects.DataAccess;

// Built to be of the CSLA EditableChildList stereotype

namespace DeadlockingCSLADemo.Objects
{
	[Serializable]
	public class CustomProperties : BusinessListBase<CustomProperties, CustomProperty>
	{

		#region Factory Methods

		private CustomProperties() 
		{
			// Enforce use of factory methods
		}

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(int personId, [Inject]ICustomPropertyRepository repository)
		{
			IList<CustomPropertyDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchListAsync(personId);
			await LoadObjectsAsync(items);
		}

		private async Task LoadObjectsAsync(IList<CustomPropertyDTO> items)
		{
			bool raiseEvents;

			raiseEvents = RaiseListChangedEvents;
			RaiseListChangedEvents = false;

			foreach (var item in items)
			{
				Add(await DataPortal.FetchChildAsync<CustomProperty>(item));
			}

			RaiseListChangedEvents = raiseEvents;
		}

		#endregion

	}
}
