using System;

namespace CslaSerialization.Core
{

	[AttributeUsage(AttributeTargets.Class)]
	public class AutoSerializableAttribute : Attribute
	{

		public AutoSerializableAttribute()
		{
		}

	}

}