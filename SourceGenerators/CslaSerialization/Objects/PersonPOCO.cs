using System;
using CslaSerialization.Core;

namespace CslaSerialization.Objects
{

	/// <summary>
	/// A class for which automatic serialization code is to be generated
	/// </summary>
	/// <remarks>The class is decorated with the AutoSerializable attribute so that it is picked up by our source generator</remarks>
	[AutoSerializable]
	public partial class PersonPOCO
	{

		private string _fieldTest = "foo";

		public int PersonId { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; }

		public string LastName { get; set; }

		[AutoSerializationExcluded]
		public string NonSerializedText { get; set; } = string.Empty;

		[AutoSerializationIncluded]
		private string PrivateSerializedText { get; set; } = string.Empty;

		public string GetPrivateSerializedText()
		{
			return PrivateSerializedText;
		}

		public void SetPrivateSerializedText(string newValue)
		{
			PrivateSerializedText = newValue;
		}

		private string PrivateText { get; set; } = string.Empty;

		public string GetPrivateText()
		{ 
			return PrivateText;
		}

		internal DateTime DateOfBirth { get; set; }

		public void SetDateOfBirth(DateTime newDateOfBirth)
		{
			DateOfBirth = newDateOfBirth;
		}

		public DateTime GetDateOfBirth()
		{
			return DateOfBirth;
		}

		public AddressPOCO Address { get; set; }

	}

}
