using System;
using ExecutionFramework;

namespace UncontrolledTrimming.ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{ 				
			ExecutionResults executionResults;
			ControlledExecutor executor;

			if (args.Length < 2)
			{
				Console.WriteLine("Usage: UncontrolledTrimming.ConsoleUI.exe fullTypeName methodName");
				return;
			}

			try
			{
				Console.WriteLine($"Execution of method '{args[1]}' has been requested on type '{args[0]}'");

				executor = new ControlledExecutor();
				executionResults = executor.Execute(args[0], args[1]);

				if (executionResults != ExecutionResults.Success)
				{
					Console.WriteLine($"Execution failed; result was '{executionResults}'");
				}
				else
				{
					Console.WriteLine("Execution is complete");
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("An exception was encountered during controlled execution!");
				Console.WriteLine(ex.ToString());
			}

			Console.WriteLine("Press a key to exit ...");
			Console.ReadKey();

		}
	}
}