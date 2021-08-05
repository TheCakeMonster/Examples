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

			netStandardMethodExists = LinkerFriendlyNetStandardLibrary.ReflectionChecker.IsMethodPresent(
				"LinkerFriendlyNetStandardLibrary.TestClass",
				"ThisMightBeRemovedByTheLinker",
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
				);
			netStandardTypeExists = LinkerFriendlyNetStandardLibrary.ReflectionChecker.IsTypePresent(
				"LinkerFriendlyNetStandardLibrary.RandomIntGenerator, LinkerFriendlyNetStandardLibrary"
				);

			net6MethodExists = LinkerFriendlyNet6Library.ReflectionChecker.IsMethodPresent(
				"LinkerFriendlyNet6Library.TestClass",
				"ThisMightBeRemovedByTheLinker",
				System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
				);
			net6TypeExists = LinkerFriendlyNetStandardLibrary.ReflectionChecker.IsTypePresent(
				"LinkerFriendlyNet6Library.RandomIntGenerator, LinkerFriendlyNet6Library"
				);

			Console.WriteLine("I expect all of the following to be true:");
			Console.WriteLine();
			Console.WriteLine($"Net Standard method exists: {netStandardMethodExists}");
			Console.WriteLine($"Net Standard type exists: {netStandardTypeExists}");
			Console.WriteLine($"Net 6 method exists: {net6MethodExists}");
			Console.WriteLine($"Net 6 type exists: {net6TypeExists}");
			Console.WriteLine();
			Console.WriteLine("Press Enter to exit the application");

			Console.ReadLine();
		}
	}
}
