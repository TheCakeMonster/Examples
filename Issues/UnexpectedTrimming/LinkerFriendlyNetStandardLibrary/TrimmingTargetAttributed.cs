using System;
using System.Diagnostics.CodeAnalysis;

namespace LinkerFriendlyNetStandardLibrary
{

	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
	public class TrimmingTargetAttributed
	{

		public static void DoSomething()
		{

		}

		private void ThisMightBeRemovedByTheLinker()
		{
			int value;
			bool success = true;
			string someStringValue = "A very long string that would need to be placed onto the heap if this method were not being removed by the linker, to see if that helps";
			RandomIntGenerator randomIntGenerator;

			if (success)
			{
				randomIntGenerator = new RandomIntGenerator();
				value = randomIntGenerator.GetRandomValue();
				someStringValue += value.ToString();
			}
		}

	}
}
