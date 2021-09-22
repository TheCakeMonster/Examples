using CslaSerialization.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Objects
{

	[AutoSerializable]
	public partial class NestingPOCO2
	{

		[AutoSerialized]
		private NestedPOCO _poco = new NestedPOCO() { Value = "Hello" };

		[AutoSerializable]
		protected internal partial class NestedPOCO
		{

			public string Value { get; set; }

		}

		public string GetValue()
		{
			return _poco.Value;
		}

		public void SetValue(string value)
		{
			_poco.Value = value;
		}

	}
}
