using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	[Serializable]
	public class NestedChildDTO
	{

		public int NestedChildId { get; set; }

		public int? ParentNestedChildId { get; set; }

		public int PersonId { get; set; }

		public string ChildName { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}