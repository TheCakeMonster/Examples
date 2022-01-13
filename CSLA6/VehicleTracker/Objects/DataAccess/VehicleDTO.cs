using System;
using System.Collections.Generic;
using System.Text;

// Generated from the built-in Scriban MutableDTO template

namespace VehicleTracker.Objects.DataAccess
{
	[Serializable]
	public class VehicleDTO
	{

		public int VehicleId { get; set; }

		public string NickName { get; set; }

		public string KeyReference { get; set; }

		public DateTime CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		public DateTime UpdatedAt { get; set; }

		public string UpdatedBy { get; set; }

	}
}