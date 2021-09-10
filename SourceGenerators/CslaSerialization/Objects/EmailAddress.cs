using Csla.Serialization.Mobile;
using System;
using System.Collections.Generic;
using System.Text;

namespace CslaSerialization.Objects
{

	[Serializable]
	public class EmailAddress : IMobileObject
	{

		public string Email {  get; set; }

		public void GetChildren(SerializationInfo info, MobileFormatter formatter)
		{
		}

		public void GetState(SerializationInfo info)
		{
			info.AddValue("Email", Email);
		}

		public void SetChildren(SerializationInfo info, MobileFormatter formatter)
		{
		}

		public void SetState(SerializationInfo info)
		{
			Email = info.GetValue<string>("Email");
		}
	}
}
