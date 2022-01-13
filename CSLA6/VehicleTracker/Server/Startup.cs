using Csla.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

// Generated from the built-in Scriban Blazor Host CSLA Startup template

namespace VehicleTracker.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// services.AddResponseCompression(options =>
			// {
			// 	options.Providers.Add<GzipCompressionProvider>();
			// 	options.Providers.Add<BrotliCompressionProvider>();
			// 	options.MimeTypes =
			// 		ResponseCompressionDefaults.MimeTypes.Concat(
			// 		new[] { "application/*", "text/*" });
			// });
			// services.Configure<GzipCompressionProviderOptions>(options =>
			// {
			// 	options.Level = CompressionLevel.Fastest;
			// });

			// Enable synchronous IO; needed for developer exception page?
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});
			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			services.AddControllersWithViews();
			services.AddRazorPages();

			// Initialise object factories
			services.AddObjectFactories();

			// Initialise the validation services
			// TODO: services.AddValidationInMemoryRepositories();

			// Register our repository implementation of choice
			services.AddMSSQLRepositories();

			// Enable access to the HTTP Context to pick up authorisation information
			services.AddHttpContextAccessor();

			// Add CSLA as an available service
			services.AddCsla(options => options
				.DataPortal()
				.AddServerSideDataPortal()
				.UseLocalProxy());
			// TODO: services.AddTransient<Csla.Data.ConnectionManager<System.Data.SqlClient.SqlConnection>>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});

		}
	}
}
