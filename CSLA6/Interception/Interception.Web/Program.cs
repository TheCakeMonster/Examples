using Csla.Configuration;
using Csla.Server;
using Interception.Library;
using Interception.Library.Interceptors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IDataPortalServer, FakeDataPortal>(); 
builder.Services.AddTransient<InterceptionManager>();
builder.Services.AddInjector<Interceptor1>();
builder.Services.AddInjector<Interceptor2>();
builder.Services.AddCsla();
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
