using CslaSerialization.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Objects
{

	[AutoSerializable]
	public partial class AddressPOCO
	{

		public string AddressLine1 {  get; set; }

		public string AddressLine2 { get; set; }

		public string Town { get; set; }

		public string County { get; set; }

		public string Postcode { get; set; }

	}
}
