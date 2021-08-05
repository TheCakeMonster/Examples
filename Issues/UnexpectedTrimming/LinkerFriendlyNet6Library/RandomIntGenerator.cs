using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkerFriendlyNet6Library
{

	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.None)]
	internal class RandomIntGenerator
	{

		public int GetRandomValue()
		{
			Random random = new Random();

			return random.Next();
		}

	}
}
