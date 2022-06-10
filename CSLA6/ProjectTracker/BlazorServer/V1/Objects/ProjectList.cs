using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using DotNotStandard.DataAccess.Core;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban CSLA ReadOnlyRootList template

namespace ProjectTracker.Objects
{
	[Serializable]
	public class ProjectList : ReadOnlyListBase<ProjectList, ProjectInfo>
	{

		#region Factory Methods

		/// <summary>
		/// Factory class for interaction with the parent type
		/// </summary>
		public class Factory
		{
			private IDataPortal<ProjectList> _dataPortal;

			public Factory(IDataPortal<ProjectList> dataPortal)
			{
				_dataPortal = dataPortal;
			}

			/// <summary>
			/// Get a list of Project from the data store
			/// </summary>
			/// <returns>The requested list of Project objects</returns>
			public async Task<ProjectList> GetProjectListAsync()
			{
				return await _dataPortal.FetchAsync();
			}

		}

		#endregion

		#region Authorisation 

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void AddObjectAuthorizationRules()
		{
			// TODO: Define authorisation rules
			Csla.Rules.BusinessRules.AddRule(typeof(ProjectList),
				new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "ProjectManager", "Administrator"));
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
		private async Task DataPortal_FetchAsync([Inject] IConnectionManager connectionManager, [Inject]IProjectRepository repository, [Inject]IChildDataPortal<ProjectInfo> childDataPortal)
		{
			IList<ProjectDTO> items;

			if (repository is null) throw new ArgumentNullException(nameof(repository));

			await using (connectionManager.StartTransaction())
            {
				items = await repository.FetchListAsync();
				await LoadObjectsAsync(items, childDataPortal);

				// Commit the completed work
				await connectionManager.CommitTransactionAsync();
            }
		}

		private async Task LoadObjectsAsync(IList<ProjectDTO> items, IChildDataPortal<ProjectInfo> childDataPortal)
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
