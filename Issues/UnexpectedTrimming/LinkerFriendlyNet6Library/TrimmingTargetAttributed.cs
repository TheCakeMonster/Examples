using System;
using System.Diagnostics.CodeAnalysis;

namespace LinkerFriendlyNet6Library
{

	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
	public class TrimmingTargetAttributed
	{

		public static void DoSomething()
		{
			//TestClass testIntance = new TestClass();
			//testIntance.ThisMightBeRemovedByTheLinker();
		}

		private void ThisMightBeRemovedByTheLinker()
		{
			int value = 0;
			bool success = true;
			string someStringValue = "This is quite a long string that isn't much use to anyone, but I don't think that entirely matters. It's just for testing.";
			RandomIntGenerator randomiser;

			if (success)
			{
				randomiser = new RandomIntGenerator();
				while (value < 1000000)
				{
					value = randomiser.GetRandomValue();
					someStringValue += value.ToString();
				}
			}
		}

	}
}
