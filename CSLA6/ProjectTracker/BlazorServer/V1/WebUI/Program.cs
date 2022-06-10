using Csla.Configuration;
using DotNotStandard.Validation.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

// Generated from the built-in Scriban Blazor Server CSLA Program template

namespace ProjectTracker.WebUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ILogger logger;

			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			// Configure validation services
			builder.Services.AddValidationSubsystem();
			builder.Services.AddValidationInMemoryRepositories();

			// Register business object factories
			builder.Services.AddObjectFactories();

			// Register our ViewModels
			builder.Services.AddViewModels();

			// Register our repository implementation of choice
			builder.Services.AddMSSQLRepositories();

			// TODO: Add appropriate user authentication and then replace/remove this test behaviour!
			builder.Services.AddFakeClaimsPrincipalSubsystem();

			// Add CSLA as an available service
			builder.Services.AddCsla(cfg => cfg
			  .AddAspNetCore()
			  .AddServerSideBlazor()
			);
			builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			// Initialise validation
			logger = app.Services.GetRequiredService<ILogger<Program>>();
			var subsystem = ValidationSubsystem.StartInitialisation(app.Services);
			while (!subsystem.TryCompleteInitialisation(TimeSpan.FromSeconds(2)))
            {
				logger.LogWarning("Waiting for initialisation of the validation subsystem");
            }

			app.Run();
		}
	}
}
