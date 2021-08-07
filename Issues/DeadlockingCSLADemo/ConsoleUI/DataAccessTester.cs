using DeadlockingCSLADemo.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.ConsoleUI
{
	internal static class DataAccessTester
	{

		internal static async Task DoTestsAsync()
		{
			int maxDOP = 1;
			Task[] tasks;

			// Setup the tests
			tasks = new Task[maxDOP];
			for (int taskNumber = 0; taskNumber < maxDOP; taskNumber++)
			{
				tasks[taskNumber] = DoIterationAsync(taskNumber);
			}

			// Run the tests and wait for completion
			await Task.WhenAll(tasks);

		}

		internal static async Task DoIterationAsync(int taskNumber)
		{
			int iteration = 0;
			Person person;

			// Run the test
			while (iteration < 10000)
			{

				// Load, modify and then save the changes
				person = await Person.GetPersonAsync(1);
				Console.WriteLine($"Task {taskNumber}: Person {iteration} loaded successfully");

				ModifyPerson(person);

				person = await person.SaveAsync();
				Console.WriteLine($"Task {taskNumber}: Person {iteration} saved successfully");

				iteration++;
			}

		}

		private static void ModifyPerson(Person person)
		{

			foreach (EmploymentHistory employmentHistory in person.EmploymentHistories)
			{
				employmentHistory.EmployerName = "Company Y";
			}

			foreach (CustomProperty customProperty in person.CustomProperties)
			{
				customProperty.PropertyValue = "123";
			}

			foreach (NestedChild nestedChild in person.NestedChildren)
			{
				nestedChild.ChildName = "ABC 123";
				ModifyNestedChild(nestedChild);
			}
		}

		private static void ModifyNestedChild(NestedChild nestedChild)
		{
			foreach (NestedChild nestedGrandChild in nestedChild.NestedChildren)
			{
				nestedGrandChild.ChildName = "Grandchild 123";
				ModifyNestedChild(nestedGrandChild);
			}
		}

	}
}
