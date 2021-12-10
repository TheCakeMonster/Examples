using BlazorExample.Client;
using BlazorExample.Shared;
using Csla;
using Csla.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient("BlazorExample.ServerAPI", 
	client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
	.AddHttpMessageHandler<MixedModeAuthorizationMessageHandler>();
builder.Services.AddTransient<MixedModeAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorExample.ServerAPI"));

builder.Services.AddApiAuthorization();

// builder.Services.AddAuthorizationCore();
// builder.Services.AddOptions();
// builder.Services.AddSingleton<AuthenticationStateProvider, CurrentUserAuthenticationStateProvider>();
// builder.Services.AddSingleton<CurrentUserService>();
// builder.Services.AddScoped<AuthenticationStateProvider, CslaRemoteAuthenticationStateProvider>();

// builder.Services.AddScoped<CslaAuthenticationStateWatcher>();

builder.Services.AddCsla(o=>o
  .WithBlazorWebAssembly()
  .DataPortal()
    .UseHttpProxy(options => options.DataPortalUrl = "/api/DataPortal"));

builder.UseCsla();

await builder.Build().RunAsync();
