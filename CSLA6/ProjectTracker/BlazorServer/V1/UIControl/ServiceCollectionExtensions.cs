using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ProjectTracker.UIControl.Navigation;

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

			// Perform auto-discovery of all ViewModels conforming to standard naming conventions
			services.DiscoverTypes()
				.Where(t => t.Name.EndsWith("ViewModel"))
				.AsThemselves()
				.Register();

			// TODO: Add registrations for any ViewModels that do not fulfil standard naming conventions
			// services.AddTransient<ManageViewModel>();

			return services;
		}

	}

}