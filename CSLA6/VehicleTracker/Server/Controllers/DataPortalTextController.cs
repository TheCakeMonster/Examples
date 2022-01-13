using System;
using System.Threading.Tasks;
using Csla;
using Microsoft.AspNetCore.Mvc;

namespace VehicleTracker.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DataPortalTextController : Csla.Server.Hosts.HttpPortalController
	{
		public DataPortalTextController(ApplicationContext applicationContext) : base(applicationContext)
		{
			// TODO: Is text serialization needed on .NET 6?
			// Text serialization is required for Blazor WASM, as binary transfers are not supported under WASM
			// UseTextSerialization = true;
		}
	}
}