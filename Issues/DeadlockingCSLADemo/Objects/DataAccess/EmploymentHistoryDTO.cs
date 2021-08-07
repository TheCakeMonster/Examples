using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	[Serializable]
	public class EmploymentHistoryDTO
	{

		public int EmploymentHistoryId { get; set; }

		public string EmployerName { get; set; }

		public DateTime JoinedOn { get; set; }

		public DateTime DepartedOn { get; set; }

		public int PersonId { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}