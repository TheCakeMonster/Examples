using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.APIServer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DoSomeSlowDataAccessController : ControllerBase
	{
		private readonly ILogger<DoSomeSlowDataAccessController> _logger;

		public DoSomeSlowDataAccessController(ILogger<DoSomeSlowDataAccessController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public int Get()
		{
			return 0;
		}
	}
}
