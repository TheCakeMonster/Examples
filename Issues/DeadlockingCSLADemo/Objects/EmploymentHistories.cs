using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using DeadlockingCSLADemo.Objects.DataAccess;

// Built to be of the EditableRoot CSLA EditableChildList stereotype

namespace DeadlockingCSLADemo.Objects
{
	[Serializable]
	public class EmploymentHistories : BusinessListBase<EmploymentHistories, EmploymentHistory>
	{

		#region Factory Methods

		private EmploymentHistories() 
		{
			// Enforce use of factory methods
		}

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(int parentId, [Inject]IEmploymentHistoryRepository repository)
		{
			IList<EmploymentHistoryDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchListAsync(parentId);
			await LoadObjectsAsync(items);
		}

		private async Task LoadObjectsAsync(IList<EmploymentHistoryDTO> items)
		{
			bool raiseEvents;

			raiseEvents = RaiseListChangedEvents;
			RaiseListChangedEvents = false;

			foreach (var item in items)
			{
				Add(await DataPortal.FetchChildAsync<EmploymentHistory>(item));
			}

			RaiseListChangedEvents = raiseEvents;
		}

		#endregion

	}
}
