using System;
using System.Collections.Generic;
using System.Text;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	[Serializable]
	public class PersonDTO
	{

		public int PersonId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}