using System;
using System.Collections.Generic;
using System.Text;

// Generated from the built-in Scriban MutableDTO template

namespace ProjectTracker.Objects.DataContracts
{
	[Serializable]
	public class AssignmentDTO
	{

		public int Id { get; set; }

		public int ProjectId { get; set; }

		public int ResourceId { get; set; }

		public int RoleId { get; set; }

		public DateTime Assigned { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}