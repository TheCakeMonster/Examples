using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using VehicleTracker.UIOrchestration;
using VehicleTracker.UIOrchestration.Navigation;

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
		public static IServiceCollection AddViewModels(this IServiceCollection services)
		{
			// Define required service registrations
			services.AddTransient<INavigator, Navigator>();

			// Add registrations for our ViewModels
			services.AddTransient<ManageVehiclesViewModel>();

			return services;
		}

	}

}