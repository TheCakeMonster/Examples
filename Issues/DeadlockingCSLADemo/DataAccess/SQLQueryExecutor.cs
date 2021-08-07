using Csla.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.DataAccess
{

	public class SQLQueryExecutor
	{
		private readonly IConfiguration _configuration;

		public SQLQueryExecutor(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task PerformDataAccessAsync()
		{

			using (var connectionManager = GetConnectionManager())
			{
				using (SqlCommand cmd = connectionManager.Connection.CreateCommand())
				{
					cmd.CommandText = "SELECT * FROM sys.sysdatabases";
					cmd.CommandType = CommandType.Text;

					// Execute the stored procedure and read the results
					using (var dr = new SafeDataReader(await cmd.ExecuteReaderAsync()))
					{
						while (dr.Read())
						{
						}
					}

				}
			}
		}

		private ConnectionManager<SqlConnection> GetConnectionManager()
		{
			return ConnectionManager<SqlConnection>.GetManager(_configuration.GetConnectionString("test"), false);
		}

	}
}
