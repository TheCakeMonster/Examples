using System;

namespace CslaSerialization.Core
{

	/// <summary>
	/// Indicate that a type should be auto serialized
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class AutoSerializableAttribute : Attribute
	{

		public AutoSerializableAttribute()
		{
		}

	}

}