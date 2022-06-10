using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public static IServiceCollection AddObjectFactories(this IServiceCollection services)
		{
			// Perform auto-discovery of all factories conforming to standard naming conventions
			services.DiscoverTypes()
				.Where(t => t.Name.Equals("Factory"))
				.AsThemselves()
				.Register();

			// TODO: Add registrations for any factories that do not fulfil standard naming conventions
			// services.AddTransient<.Factory>();

			return services;
		}
	}
}
