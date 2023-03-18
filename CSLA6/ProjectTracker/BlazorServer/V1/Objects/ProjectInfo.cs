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

		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
		public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(nameof(Description));
		public static readonly PropertyInfo<DateTime> StartedProperty = RegisterProperty<DateTime>(nameof(Started));
		public static readonly PropertyInfo<DateTime> EndedProperty = RegisterProperty<DateTime>(nameof(Ended));
		public static readonly PropertyInfo<DateTime> CreatedAtProperty = RegisterProperty<DateTime>(nameof(CreatedAt));
		public static readonly PropertyInfo<string> CreatedByProperty = RegisterProperty<string>(nameof(CreatedBy));
		public static readonly PropertyInfo<DateTime> UpdatedAtProperty = RegisterProperty<DateTime>(nameof(UpdatedAt));
		public static readonly PropertyInfo<string> UpdatedByProperty = RegisterProperty<string>(nameof(UpdatedBy));
		
		#region Exposed Properties and Methods

		public int Id
		{
			get { return GetProperty(IdProperty); }
		}

		public string Name
		{
			get { return GetProperty(NameProperty); }
		}

		public string Description
		{
			get { return GetProperty(DescriptionProperty); }
		}

		public DateTime Started
		{
			get { return GetProperty(StartedProperty); }
		}

		public DateTime Ended
		{
			get { return GetProperty(EndedProperty); }
		}

		public DateTime CreatedAt
		{
			get { return GetProperty(CreatedAtProperty); }
		}

		public string CreatedBy
		{
			get { return GetProperty(CreatedByProperty); }
		}

		public DateTime UpdatedAt
		{
			get { return GetProperty(UpdatedAtProperty); }
		}

		public string UpdatedBy
		{
			get { return GetProperty(UpdatedByProperty); }
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
			LoadProperty(IdProperty, data.Id);
			LoadProperty(NameProperty, data.Name);
			LoadProperty(DescriptionProperty, data.Description);
			LoadProperty(StartedProperty, data.Started);
			LoadProperty(EndedProperty, data.Ended);
			LoadProperty(CreatedAtProperty, data.CreatedAt);
			LoadProperty(CreatedByProperty, data.CreatedBy);
			LoadProperty(UpdatedAtProperty, data.UpdatedAt);
			LoadProperty(UpdatedByProperty, data.UpdatedBy);
			// Complete the load by requesting children load themselves

			return Task.CompletedTask;
		}

		#endregion

	}
}