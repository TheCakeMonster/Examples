using Csla.Serialization;

namespace CslaSerialization.Objects
{

	[AutoSerializable]
	internal partial struct Point
	{

		public int X { get; set; }

		public int Y { get; set; }

	}
}
