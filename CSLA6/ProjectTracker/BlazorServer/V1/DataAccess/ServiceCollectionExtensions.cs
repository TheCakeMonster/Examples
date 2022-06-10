using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ProjectTracker.DataAccess;

// Generated from the built-in Scriban .NET 6 Data Access ServiceCollectionExtensions template

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
		public static IServiceCollection AddMSSQLRepositories(this IServiceCollection services)
		{
			// Define required service registrations
			services.AddMSSQLConnectionManagement();

			// Perform auto-discovery of all Repository implementations conforming to standard naming conventions
			services.DiscoverTypes()
				.Where(t => t.Name.EndsWith("Repository"))
				.AsSimilarlyNamedInterface()
				.Register();

			// TODO: Add registrations for any Repository classes that do not conform to standard naming conventions
			// services.AddTransient<IRepository, Repository>();

			return services;
		}

	}

}