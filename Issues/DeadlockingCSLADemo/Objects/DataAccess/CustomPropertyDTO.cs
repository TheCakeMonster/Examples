using System;
using System.Collections.Generic;
using System.Text;

// Generated from the built-in Scriban MutableDTO template

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	[Serializable]
	public class CustomPropertyDTO
	{

		public int CustomPropertyId { get; set; }

		public int PersonId { get; set; }

		public string PropertyName { get; set; }

		public string PropertyValue { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}