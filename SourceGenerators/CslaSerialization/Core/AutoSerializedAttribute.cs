using System;

namespace CslaSerialization.Core
{

	/// <summary>
	/// Indicate that a non-public field or property should be included in auto serialization
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AutoSerializedAttribute : Attribute
	{

		public AutoSerializedAttribute()
		{

		}

	}

}