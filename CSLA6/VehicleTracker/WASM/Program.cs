using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Csla.Blazor.Client.Authentication;
using Csla.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Generated from the built-in Scriban Blazor WASM CSLA Program template

namespace VehicleTracker.WASM
{
	public class Program
	{
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient 
                { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // TODO: Add appropriate user authentication and then enable core authorization again!
            // builder.Services.AddAuthorizationCore();
            builder.Services.AddOptions();
            builder.Services.AddSingleton<AuthenticationStateProvider, CslaAuthenticationStateProvider>();
            builder.Services.AddSingleton<CslaUserService>();
            builder.Services.AddCsla(c => c
                .WithBlazorWebAssembly()
                .DataPortal()
                .UseHttpProxy(opts => 
                    opts.DataPortalUrl = $"{builder.HostEnvironment.BaseAddress }api/dataportaltext/")
            );

            builder.Services.AddViewModels();

            // Initialise object factories
            builder.Services.AddObjectFactories();

            builder.Services.AddLocalization();

            // TODO: Add appropriate user authentication and then replace this test behaviour!
			//List<Claim> claims = new List<Claim>();
			//claims.Add(new Claim(ClaimTypes.Role, "Users"));
			//Csla.ApplicationContext.User = new ClaimsPrincipal(new ClaimsIdentity(new GenericIdentity("Test User"), claims));

            await builder.Build().RunAsync();
        }

        private static void ConfigureHttpProxy(Csla.Channels.Http.HttpProxyOptions proxyOptions)
		{

		}
	}
}
