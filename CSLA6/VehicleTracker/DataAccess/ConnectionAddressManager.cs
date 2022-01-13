using Microsoft.Extensions.Configuration;
using System;

// Generated from the built-in Scriban ConnectionAddressManager template

namespace VehicleTracker.DataAccess
{
	public class ConnectionAddressManager
	{

		private readonly IConfiguration _configuration;

		public ConnectionAddressManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GetDBConnectionString(string databaseName)
		{
			return _configuration.GetConnectionString(databaseName);
		}

	}
}