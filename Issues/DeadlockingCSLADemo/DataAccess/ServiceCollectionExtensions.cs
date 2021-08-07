using DeadlockingCSLADemo.DataAccess;
using DeadlockingCSLADemo.Objects.DataAccess;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{

	/// <summary>
	/// Default DI registrations for classes defined within this assembly
	/// </summary>
	public static class ServiceCollectionExtensions
	{

		/// <summary>
		/// Add default service registrations intended for normal runtime operation
		/// </summary>
		/// <param name="services">The IServiceCollection to which to register services</param>
		/// <returns>IServiceCollection, to support method chaining if required</returns>
		public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services)
		{
			// Add registrations for our Repository classes
			services.AddTransient<IPersonRepository, PersonRepository>();
			services.AddTransient<ICustomPropertyRepository, CustomPropertyRepository>();
			services.AddTransient<IEmploymentHistoryRepository, EmploymentHistoryRepository>();
			services.AddTransient<INestedChildRepository, NestedChildRepository>();

			return services;
		}

	}

}