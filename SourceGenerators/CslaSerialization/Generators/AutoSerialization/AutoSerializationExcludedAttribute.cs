using System;

namespace CslaSerialization.Generators.AutoSerialization
{

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AutoSerializationExcludedAttribute : Attribute
	{
		public AutoSerializationExcludedAttribute()
		{
		}

	}

}