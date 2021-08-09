using Csla.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.ConsoleUI
{
	class Program
	{
		static async Task Main(string[] args)
		{
			bool valid = false;
			int maxDOP = 1;
			string maxDOPEntry;
			IServiceCollection services = new ServiceCollection();
			IServiceProvider serviceProvider;
			DataAccessTester tester;

			// Configure DI
			ConfigureServices(services, args);

			// Build the configured service provider
			serviceProvider = services.BuildServiceProvider();

			// Configure the application pipeline
			Configure(serviceProvider);

			// Enforce restricted resources to demonstrate the problem
			ThreadPool.SetMaxThreads(10, 50000);

			while (!valid)
			{
				Console.WriteLine("Enter the degree of parallelism (an integer) and press 'Enter' to start");
				maxDOPEntry = Console.ReadLine();
				valid = int.TryParse(maxDOPEntry, out maxDOP) && maxDOP > 0;
			}

			// Start the test
			tester = new DataAccessTester(maxDOP);
			await tester.DoTestsAsync();

			Console.WriteLine("Tests are complete. Press 'Enter' to quit");
			Console.ReadLine();

		}

		private static void ConfigureServices(IServiceCollection services, string[] args)
		{
			IConfiguration configuration;

			services.AddHttpClient("backend", c =>
			{
				c.BaseAddress = new Uri("https://localhost:44329");
			});

			configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();
			services.AddSingleton<IConfiguration>(configuration);

			// Register the repositories
			services.AddDataAccessRepositories();

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
