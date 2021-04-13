using Csla.Serialization.Mobile;
using CslaSerialization.Objects;
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
			Person person = new Person();
			person.PersonId = 5;

			// Act
			mobileObject = person as IMobileObject;
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
			Person person = new Person();
			person.FirstName = "Joe";

			// Act
			mobileObject = person as IMobileObject;
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
			Person person = new Person();
			person.LastName = "Smith";

			// Act
			mobileObject = person as IMobileObject;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<string>("LastName");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void GetState_WithDateOfBirth210412_ReturnsInfoContaining210412()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			DateTime actual;
			DateTime expected = new DateTime(2021, 04, 12, 16, 57, 53);
			IMobileObject mobileObject;
			Person person = new Person();
			person.SetDateOfBirth( new DateTime(2021, 04, 12, 16, 57, 53));

			// Act
			mobileObject = person as IMobileObject;
			mobileObject.GetState(serializationInfo);
			actual = serializationInfo.GetValue<DateTime>("DateOfBirth");

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

		#region SetState

		[TestMethod]
		public void SetState_WithPersonId5_ReturnsPersonWithId5()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			int actual;
			int expected = 5;
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 5);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.PersonId;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithFirstNameJoe_ReturnsPersonWithFirstNameJoe()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Joe";
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "Joe");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.FirstName;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithLastNameSmith_ReturnsPersonWithLastNameSmith()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Smith";
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "Smith");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.LastName;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithDateOfBirth210412_ReturnsPersonWithDateOfBirth210412()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			DateTime actual;
			DateTime expected = new DateTime(2021, 04, 12, 18, 27, 43);
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", new DateTime(2021, 04, 12, 18, 27, 43));
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.GetDateOfBirth();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithNonSerializedTextFred_ReturnsEmptyString()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = string.Empty;
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("NonSerializedText", "Fred");
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.NonSerialisedText;

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithPrivateTextFred_ReturnsEmptyString()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = string.Empty;
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("NonSerializedText", "Fred");
			serializationInfo.AddValue("PrivateText", "Fred");
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.GetPrivateText();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void SetState_WithPrivateSerializedTextFred_ReturnsFred()
		{

			// Arrange
			SerializationInfo serializationInfo = new SerializationInfo();
			string actual;
			string expected = "Fred";
			Person person = new Person();
			IMobileObject mobileObject;

			// Act
			serializationInfo.AddValue("PersonId", 0);
			serializationInfo.AddValue("FirstName", "");
			serializationInfo.AddValue("LastName", "");
			serializationInfo.AddValue("DateOfBirth", DateTime.MinValue);
			serializationInfo.AddValue("NonSerializedText", "");
			serializationInfo.AddValue("PrivateText", "");
			serializationInfo.AddValue("PrivateSerializedText", "Fred");
			mobileObject = person as IMobileObject;
			mobileObject.SetState(serializationInfo);
			actual = person.GetPrivateSerializedText();

			// Assert
			Assert.AreEqual(expected, actual);

		}

		#endregion

	}
}
