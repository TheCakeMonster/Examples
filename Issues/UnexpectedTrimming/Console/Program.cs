using System;

namespace TrimmingTest.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			bool netStandardMethodExists;
			bool netStandardTypeExists;
			bool net6MethodExists;
			bool net6TypeExists;

			LinkerFriendlyNetStandardLibrary.TrimmingTargetAttributed.DoSomething();
			LinkerFriendlyNet6Library.TrimmingTargetAttributed.DoSomething();

			netStandardTypeExists = TrimmingChecker.PresenceTester.IsTypePresent(
				"LinkerFriendlyNetStandardLibrary.UnusedType, LinkerFriendlyNetStandardLibrary"
				);

			net6TypeExists = TrimmingChecker.PresenceTester.IsTypePresent(
				"LinkerFriendlyNet6Library.UnusedType, LinkerFriendlyNet6Library"
				);

			Console.WriteLine("After publishing, I expect all of the following to be false:");
			Console.WriteLine();
			Console.WriteLine($"Net Standard type 'UnusedType' exists: {netStandardTypeExists}");
			Console.WriteLine($"Net 6 type 'UnusedType' exists: {net6TypeExists}");
			Console.WriteLine();

			netStandardMethodExists = TrimmingChecker.PresenceTester.IsMethodPresent(
				"LinkerFriendlyNetStandardLibrary.TrimmingTargetAttributed, LinkerFriendlyNetStandardLibrary",
				"ThisMightBeRemovedByTheLinker",
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
				);
			netStandardTypeExists = TrimmingChecker.PresenceTester.IsTypePresent(
				"LinkerFriendlyNetStandardLibrary.RandomIntGenerator, LinkerFriendlyNetStandardLibrary"
				);

			net6MethodExists = TrimmingChecker.PresenceTester.IsMethodPresent(
				"LinkerFriendlyNet6Library.TrimmingTargetAttributed, LinkerFriendlyNet6Library",
				"ThisMightBeRemovedByTheLinker",
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
				);
			net6TypeExists = TrimmingChecker.PresenceTester.IsTypePresent(
				"LinkerFriendlyNet6Library.RandomIntGenerator, LinkerFriendlyNet6Library"
				);

			Console.WriteLine("After publishing, I expect all of the following to be true:");
			Console.WriteLine();
			Console.WriteLine($"Net Standard method 'ThisMightBeRemovedByTheLinker' exists: {netStandardMethodExists}");
			Console.WriteLine($"Net Standard type 'RandomIntGenerator' exists: {netStandardTypeExists}");
			Console.WriteLine($"Net 6 method 'ThisMightBeRemovedByTheLinker' exists: {net6MethodExists}");
			Console.WriteLine($"Net 6 type 'RandomIntGenerator' exists: {net6TypeExists}");
			Console.WriteLine();
			Console.WriteLine("Press Enter to exit the application");

			Console.ReadLine();
		}
	}
}
