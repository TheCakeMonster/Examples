using System;

namespace CslaSerialization.Core
{

	/// <summary>
	/// Indicate that a public field or property should be excluded from auto serialization
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AutoNonSerializedAttribute : Attribute
	{
		public AutoNonSerializedAttribute()
		{
		}

	}

}