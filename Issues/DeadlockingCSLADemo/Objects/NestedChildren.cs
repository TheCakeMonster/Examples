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
	public class NestedChildren : BusinessListBase<NestedChildren, NestedChild>
	{

		#region Factory Methods

		private NestedChildren() 
		{
			// Enforce use of factory methods
		}

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(int personId, [Inject]INestedChildRepository repository)
		{
			IList<NestedChildDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchListAsync(personId);
			await LoadObjectsAsync(items);
		}

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(NestedChildDTO parentNestedChild, [Inject]INestedChildRepository repository)
		{
			IList<NestedChildDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchNestedListAsync(parentNestedChild.NestedChildId);
			await LoadObjectsAsync(items);
		}

		private async Task LoadObjectsAsync(IList<NestedChildDTO> items)
		{
			bool raiseEvents;

			raiseEvents = RaiseListChangedEvents;
			RaiseListChangedEvents = false;

			foreach (var item in items)
			{
				Add(await DataPortal.FetchChildAsync<NestedChild>(item));
			}

			RaiseListChangedEvents = raiseEvents;
		}

		#endregion

	}
}
