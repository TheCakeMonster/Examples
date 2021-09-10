using Csla.Serialization.Mobile;
using CslaSerialization.Objects;
using CslaSerialization.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CslaSerialization.UnitTests
{
	[TestClass]
	public class PersonTests
	{

		#region GetState

		[TestMethod]
		public void GetState_WithPersonId5_ReturnsInfoContaining5()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			int actual;
			int expected = 5;
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.PersonId = 5;

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<int>("PersonId");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithFirstNameJoe_ReturnsInfoContainingJoe()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Joe";
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.FirstName = "Joe";

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<string>("FirstName");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithLastNameSmith_ReturnsInfoContainingSmith()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Smith";
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.LastName = "Smith";

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<string>("LastName");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithInternalDateOfBirth210412_ReturnsInfoWithoutDateOfBirth()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			bool actual;
			bool expected = false;
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.SetDateOfBirth( new DateTime(2021, 04, 12, 16, 57, 53));

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.Values.ContainsKey("DateOfBirth");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithMiddleNameMid_ReturnsInfoWithoutMiddleName()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			bool actual;
			bool expected = false;
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.SetMiddleName("Mid");

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.Values.ContainsKey("MiddleName");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithFieldMiddleNameMid_ReturnsInfoContainingMid()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Mid";
			IMobileObject mobileObject;
			PersonPOCO person = new PersonPOCO();
			person.SetMiddleName("Mid");

			// Act
			mobileObject = (IMobileObject)person;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<string>("_middleName");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#region SetState

		[TestMethod]
		public void SetState_WithPersonId5_ReturnsPersonWithId5()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			int actual;
			int expected = 5;
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["PersonId"].Value = 5;
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.PersonId;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithFirstNameJoe_ReturnsPersonWithFirstNameJoe()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = "Joe";
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["FirstName"].Value = "Joe";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.FirstName;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithLastNameSmith_ReturnsPersonWithLastNameSmith()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = "Smith";
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["LastName"].Value = "Smith";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.LastName;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithInternalDateOfBirth210412_ReturnsPersonWithNoDateOfBirth()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			DateTime actual;
			DateTime expected = DateTime.MinValue;
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["DateOfBirth"].Value = new DateTime(2021, 04, 12, 18, 27, 43);
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.GetDateOfBirth();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithNonSerializedTextFred_ReturnsEmptyString()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = string.Empty;
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["NonSerializedText"].Value = "Fred";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.NonSerializedText;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithPrivateTextFred_ReturnsEmptyString()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = string.Empty;
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["PrivateText"].Value = "Fred";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.GetPrivateText();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithPrivateSerializedTextFred_ReturnsFred()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = "Fred";
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["PrivateSerializedText"].Value = "Fred";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.GetPrivateSerializedText();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithIncludedMiddleNameFieldMid_ReturnsMiddleNamePropertyMid()
		{

			// Arrange
			SerializationInfo serializationInfo = PersonSerializationInfoFactory.GetDefaultSerializationInfo();
			string actual;
			string expected = "Mid";
			PersonPOCO person = new PersonPOCO();
			IMobileObject mobileObject;

			// Act
			serializationInfo.Values["_middleName"].Value = "Mid";
			mobileObject = (IMobileObject)person;
			mobileObject.SetState(serializationInfo);
			actual = person.MiddleName;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

	}
}
