using System;

namespace CslaSerialization.Core
{

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class AutoSerializationExcludedAttribute : Attribute
	{
		public AutoSerializationExcludedAttribute()
		{
		}

	}

}