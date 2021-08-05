using Csla.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.ConsoleUI
{
	class Program
	{
		static async Task Main(string[] args)
		{
			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider;

			// Configure DI
			ConfigureServices(services);

			// Build the configured service provider
			serviceProvider = services.BuildServiceProvider();

			// Configure the application pipeline
			Configure(serviceProvider);

			await DataAccessTester.DoTestsAsync();

		}

		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddHttpClient("backend", c =>
			{
				c.BaseAddress = new Uri("https://localhost:44329");
			});

			// Register the repositories
			services.AddDataAccessRepositories();

			// Initialise the validation services
			services.AddValidationInMemoryRepositories();

			// Configure CSLA to use the local proxy
			CslaConfiguration.Configure().
				DataPortal().DefaultProxy(typeof(Csla.DataPortalClient.LocalProxy), "");

			// TODO: Add appropriate user authentication and then replace this test behaviour!
			//CslaConfiguration.Configure().ContextManager(new Csla.Blazor.ApplicationContextManager());
			CslaConfiguration.Configure().ContextManager(new TestApplicationContextManager());

			// Add CSLA as an available service
			services.AddCsla();

		}

		private static void Configure(IServiceProvider serviceProvider)
		{

			CslaConfiguration.Configure().ServiceProviderScope(serviceProvider);
		}

	}
}
