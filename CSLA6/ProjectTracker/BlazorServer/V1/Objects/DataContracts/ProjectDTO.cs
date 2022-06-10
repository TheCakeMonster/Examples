using System;
using System.Collections.Generic;
using System.Text;

// Generated from the built-in Scriban MutableDTO template

namespace ProjectTracker.Objects.DataContracts
{
	[Serializable]
	public class ProjectDTO
	{

		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Started { get; set; }

		public DateTime Ended { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}