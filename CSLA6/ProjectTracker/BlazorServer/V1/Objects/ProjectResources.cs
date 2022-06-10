using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using DotNotStandard.DataAccess.Core;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban CSLA EditableChildList template

namespace ProjectTracker.Objects
{
	[Serializable]
	public class ProjectResources : BusinessListBase<ProjectResources, ProjectResourceEdit>
	{

		#region Factory Methods

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(int parentId, [Inject]IAssignmentRepository repository, [Inject]IChildDataPortal<ProjectResourceEdit> childDataPortal)
		{
			IList<AssignmentDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			items = await repository.FetchListAsync(parentId);
			await LoadObjectsAsync(items, childDataPortal);
		}

		private async Task LoadObjectsAsync(IList<AssignmentDTO> items, IChildDataPortal<ProjectResourceEdit> childDataPortal)
		{
			bool raiseEvents;

			raiseEvents = RaiseListChangedEvents;
			RaiseListChangedEvents = false;

			foreach (var item in items)
			{
				Add(await childDataPortal.FetchChildAsync(item));
			}

			RaiseListChangedEvents = raiseEvents;
		}

		#endregion

	}
}
