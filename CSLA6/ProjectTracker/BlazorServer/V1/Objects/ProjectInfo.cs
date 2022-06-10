using System;
using System.Threading.Tasks;
using Csla;
using DotNotStandard.DataAccess.Core;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban CSLA ReadOnlyChild template

namespace ProjectTracker.Objects
{
	[Serializable]
	public class ProjectInfo : ReadOnlyBase<ProjectInfo>
	{

		private static readonly PropertyInfo<int> _idProperty = RegisterProperty<int>(nameof(Id));
		private static readonly PropertyInfo<string> _nameProperty = RegisterProperty<string>(nameof(Name));
		private static readonly PropertyInfo<string> _descriptionProperty = RegisterProperty<string>(nameof(Description));
		private static readonly PropertyInfo<DateTime> _startedProperty = RegisterProperty<DateTime>(nameof(Started));
		private static readonly PropertyInfo<DateTime> _endedProperty = RegisterProperty<DateTime>(nameof(Ended));
		private static readonly PropertyInfo<DateTime> _createdAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		private static readonly PropertyInfo<string> _createdByProperty = RegisterProperty<string>(nameof(CreatedBy));
		private static readonly PropertyInfo<DateTime> _updatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		private static readonly PropertyInfo<string> _updatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		
		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(_idProperty); }
		}

		public string Name
		{
			get { return GetProperty(_nameProperty); }
		}

		public string Description
		{
			get { return GetProperty(_descriptionProperty); }
		}

		public DateTime Started
		{
			get { return GetProperty(_startedProperty); }
		}

		public DateTime Ended
		{
			get { return GetProperty(_endedProperty); }
		}

		public DateTime CreatedAt
		{
			get { return GetProperty(_createdAtProperty); }
		}

		public string CreatedBy
		{
			get { return GetProperty(_createdByProperty); }
		}

		public DateTime UpdatedAt
		{
			get { return GetProperty(_updatedAtProperty); }
		}

		public string UpdatedBy
		{
			get { return GetProperty(_updatedByProperty); }
		}


		#endregion

		#region Factory Methods

		#endregion

		#region Data Access

		[FetchChild]
		private async Task DataPortal_FetchChildAsync(ProjectDTO data)
		{
			await LoadObjectAsync(data);
		}

		private Task LoadObjectAsync(ProjectDTO data)
		{
			LoadProperty(_idProperty, data.Id);
			LoadProperty(_nameProperty, data.Name);
			LoadProperty(_descriptionProperty, data.Description);
			LoadProperty(_startedProperty, data.Started);
			LoadProperty(_endedProperty, data.Ended);
			LoadProperty(_createdAtProperty, data.CreatedAt);
			LoadProperty(_createdByProperty, data.CreatedBy);
			LoadProperty(_updatedAtProperty, data.UpdatedAt);
			LoadProperty(_updatedByProperty, data.UpdatedBy);
			// Complete the load by requesting children load themselves

			return Task.CompletedTask;
		}

		#endregion

	}
}