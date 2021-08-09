using DeadlockingCSLADemo.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.ConsoleUI
{
	internal class DataAccessTester
	{

		private readonly int _maxDOP;

		public DataAccessTester(int maxDOP)
		{
			_maxDOP = maxDOP;
		}

		internal async Task DoTestsAsync()
		{
			Task[] tasks;

			// Setup the tests
			tasks = new Task[_maxDOP + 1];
			tasks[0] = DoMonitoringIterationAsync();
			for (int taskNumber = 1; taskNumber < _maxDOP + 1; taskNumber++)
			{
				tasks[taskNumber] = DoDataAccessIterationAsync(taskNumber);
			}

			// Run the tests and wait for completion
			await Task.WhenAll(tasks);

		}

		internal async Task DoDataAccessIterationAsync(int taskNumber)
		{
			int iteration = 0;
			Person person;

			// Run the test
			while (iteration < 10)
			{

				// Load, modify and then save the changes
				Console.WriteLine($"Task {taskNumber}: Loading person {iteration}");
				person = await Person.GetPersonAsync(1);
				Console.WriteLine($"Task {taskNumber}: Person {iteration} loaded successfully");

				Console.WriteLine($"Task {taskNumber}: Modifying person {iteration}");
				ModifyPerson(person);
				Console.WriteLine($"Task {taskNumber}: Person {iteration} modified successfully");

				Console.WriteLine($"Task {taskNumber}: Saving person {iteration}");
				person = await person.SaveAsync();
				Console.WriteLine($"Task {taskNumber}: Person {iteration} saved successfully");

				iteration++;
			}

			Console.WriteLine($"Task {taskNumber}: All iterations completed");
		}

		private async Task DoMonitoringIterationAsync()
		{
			int iteration = 0;
			
			while (iteration < 100)
			{
				await Task.Delay(300);
				Console.WriteLine($"Task 0: Monitoring iteration {iteration} completed");

				iteration++;
			}

			Console.WriteLine($"Task 0: All monitoring iterations completed");
		}

		private void ModifyPerson(Person person)
		{

			foreach (NestedChild nestedChild in person.NestedChildren)
			{
				nestedChild.ChildName = "ABC 123";
				ModifyNestedChild(nestedChild);
			}
		}

		private void ModifyNestedChild(NestedChild nestedChild)
		{
			foreach (NestedChild nestedGrandChild in nestedChild.NestedChildren)
			{
				nestedGrandChild.ChildName = "Grandchild 123";
				ModifyNestedChild(nestedGrandChild);
			}
		}

	}
}
