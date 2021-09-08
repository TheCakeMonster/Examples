using System;

namespace CslaSerialization.Generators.AutoSerialization
{

	[AttributeUsage(AttributeTargets.Class)]
	public class AutoSerializableAttribute : Attribute
	{

		public AutoSerializableAttribute()
		{
		}

	}

}