using System;

namespace CslaSerialization.Core
{

	/// <summary>
	/// Indicate that a class should be auto serialized
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AutoSerializableAttribute : Attribute
	{

		public AutoSerializableAttribute()
		{
		}

	}

}