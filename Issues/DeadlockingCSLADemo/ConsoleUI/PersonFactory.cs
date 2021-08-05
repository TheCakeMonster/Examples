using DeadlockingCSLADemo.Objects;
using System;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.ConsoleUI
{
	internal class PersonFactory
	{
		internal static async Task<Person> CreatePersonAsync()
		{
			Person person;
			EmploymentHistory employmentHistory;
			CustomProperty customProperty; 

			person = await Person.NewPersonAsync();
			person.FirstName = "FirstName";
			person.LastName = "LastName";

			for (int iteration = 0; iteration < 10; iteration++)
			{
				employmentHistory = person.EmploymentHistories.AddNew();
				employmentHistory.EmployerName = $"Company {iteration}";
				employmentHistory.JoinedOn = DateTime.Now.AddYears(-15 + iteration);
				employmentHistory.DepartedOn = DateTime.Now.AddYears(-14 + iteration);
			}

			for (int iteration = 0; iteration < 30; iteration++)
			{
				customProperty = person.CustomProperties.AddNew();
				customProperty.PropertyName = $"Property {iteration}";
				customProperty.PropertyValue = $"{iteration}";
			}

			return person;
		}
	}
}