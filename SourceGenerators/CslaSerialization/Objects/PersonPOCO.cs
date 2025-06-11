﻿using Csla.Serialization;
using System;

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
		private string _lastName;

		[AutoSerialized]
		private string _middleName;

		public int PersonId { get; set; }

		public string FirstName { get; set; }

		public string MiddleName => _middleName;

		public void SetMiddleName(string middleName)
		{
			_middleName = middleName;
		}

		public string LastName
		{
			get { return _lastName; }
			set {  _lastName = value; }
		}

		[AutoSerialized]
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

		public string GetUnderlyingPrivateText()
		{ 
			return PrivateText;
		}

		public void SetUnderlyingPrivateText(string value)
		{
			PrivateText = value;
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

		public EmailAddress EmailAddress {  get; set; }	

	}

}
