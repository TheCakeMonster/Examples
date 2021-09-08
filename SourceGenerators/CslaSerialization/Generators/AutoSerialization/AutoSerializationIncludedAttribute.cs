using System;

namespace CslaSerialization.Generators.AutoSerialization
{

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AutoSerializationIncludedAttribute : Attribute
	{

		public AutoSerializationIncludedAttribute()
		{

		}

	}

}