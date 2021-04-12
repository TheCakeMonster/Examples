using System;
using CslaSerialization.Core;

namespace CslaSerialization.Objects
{

	/// <summary>
	/// A class for which automatic serialization code is to be generated
	/// </summary>
	/// <remarks>The class is decorated with the AutoSerializable attribute so that it is picked up by our source generator</remarks>
	[AutoSerializable]
	public partial class Person
	{

		public int PersonId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

	}

}
