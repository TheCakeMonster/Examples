using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LinkerFriendlyNetStandardLibrary
{

	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.None)]
	internal class RandomIntGenerator
	{

		internal int GetRandomValue()
		{
			Random random = new Random();

			return random.Next();
		}

	}
}
