// This is the definition of the class we are trying to generate

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Csla;
//using Csla.Serialization.Mobile;

//namespace CslaSerialization.Objects
//{

//	[Serializable]
//	public partial class Person : IMobileObject
//	{
//
//		public void GetChildren(SerializationInfo info, MobileFormatter formatter)
//		{
//		}

//		public void GetState(SerializationInfo info)
//		{
//			info.AddValue(nameof(FirstName), FirstName);
//			info.AddValue(nameof(LastName), LastName);
//			info.AddValue(nameof(DateOfBirth), DateOfBirth);
//		}

//		public void SetChildren(SerializationInfo info, MobileFormatter formatter)
//		{
//		}

//		public void SetState(SerializationInfo info)
//		{
//			FirstName = info.GetValue<string>(nameof(FirstName));
//			LastName = info.GetValue<string>(nameof(LastName));
//			DateOfBirth = info.GetValue<DateTime>(nameof(DateOfBirth));
//		}
//
//	}

//}
