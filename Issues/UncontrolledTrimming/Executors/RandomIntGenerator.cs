using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executors
{
	internal static class RandomIntGenerator
	{

		internal static int Generate()
		{
			Random random;

			random = new Random();
			return random.Next();
		}

	}
}
