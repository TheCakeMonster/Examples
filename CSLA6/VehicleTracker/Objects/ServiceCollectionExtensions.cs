using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VehicleTracker.Objects;

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
			// Define required service registrations
			services.AddTransient<IVehicleFactory, VehicleFactory>();
			services.AddTransient<IVehicleListFactory, VehicleListFactory>();

			return services;
		}

	}
}