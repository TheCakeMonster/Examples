using CslaSerialization.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Objects
{

	[AutoSerializable]
	internal partial struct Point
	{

		public int X { get; set; }

		public int Y { get; set; }

	}
}
