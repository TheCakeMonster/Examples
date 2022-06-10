using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using DotNotStandard.DataAccess.Core;
using Csla.Data;
using ProjectTracker.Objects.DataContracts;

// Generated from the built-in Scriban StoredProcedureRepository template

namespace ProjectTracker.DataAccess
{
	public class ProjectRepository : IProjectRepository
	{
		
		private readonly IConnectionManager _connectionManager;
		
		#region Constructors
		
		public ProjectRepository(IConnectionManager connectionManager)
		{
			_connectionManager = connectionManager;
		}

		#endregion

		#region Exposed Methods

		/// <summary>
		/// Get a list of items from the data store
		/// </summary>
		/// <returns>A list of DTOs representing the required data</returns>
		public async Task<IList<ProjectDTO>> FetchListAsync()
		{
			int returnValue;
			IList<ProjectDTO> list = new List<ProjectDTO>();
			SqlConnection connection;

			connection = await _connectionManager.GetConnectionAsync<SqlConnection>("ProjectTracker");
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = "spProjectList_Fetch";
				cmd.CommandType = CommandType.StoredProcedure;
				AddReturnParameter(cmd);

				// Execute the stored procedure and read the results
				using(var dr = new SafeDataReader(await cmd.ExecuteReaderAsync()))
				{
					while (dr.Read())
					{
						list.Add(LoadDTO(dr, Fieldset.LightweightList));
					}
				}

				// Check the return value to ensure correct execution in the SP
				returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
				switch (returnValue)
				{
					case 0: break;
					case 1: throw new UnknownDatabaseException();
					case 2: throw new IncorrectRowCountException();
					default: throw new UnknownReturnValueException();
				}
			}

			return list;
		}

		/// <summary>
		/// Get a single item from the data store using the unique identifiers provided
		/// </summary>
		/// <returns>A DTO containing the data for the requested item</returns>
		public async Task<ProjectDTO> FetchAsync(int id)
		{
			int returnValue;
			ProjectDTO data = null;
			SqlConnection connection;

			connection = await _connectionManager.GetConnectionAsync<SqlConnection>("ProjectTracker");
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = "spProject_Fetch";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", id);
				AddReturnParameter(cmd);

				// Execute the stored procedure and read the results
				using(var dr = new SafeDataReader(await cmd.ExecuteReaderAsync()))
				{
					if (dr.Read())
					{
						data = LoadDTO(dr, Fieldset.All);
					}
				}

				// Check the return value to ensure correct execution in the SP
				returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
				switch(returnValue)
				{
					case 0: break;
					case 1: throw new UnknownDatabaseException();
					case 2: throw new IncorrectRowCountException();
					default: throw new UnknownReturnValueException();
				}
			}

			return data;
		}

		/// <summary>
		/// Insert a new item into the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<ProjectDTO> InsertAsync(ProjectDTO data)
		{
			int returnValue;
			int rowsAffected;
			SqlConnection connection;

			connection = await _connectionManager.GetConnectionAsync<SqlConnection>("ProjectTracker");
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = "spProject_Insert";
				cmd.CommandType = CommandType.StoredProcedure;
				AddInsertParametersWithValues(cmd, data);

				// Execute the stored procedure and read the results
				rowsAffected = await cmd.ExecuteNonQueryAsync();

				// Check the return value to ensure correct execution in the SP
				returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
				switch (returnValue)
				{
					case 0: break;
					case 1: throw new UnknownDatabaseException();
					case 2: throw new IncorrectRowCountException();
					default: throw new UnknownReturnValueException();
				}

				// Update the DTO with any values that were changed
				UpdateDTOForManagedFields(cmd, data);

			}

			return data;
		}

		/// <summary>
		/// Update an existing item in the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<ProjectDTO> UpdateAsync(ProjectDTO data)
		{
			int returnValue;
			int rowsAffected;
			SqlConnection connection;

			connection = await _connectionManager.GetConnectionAsync<SqlConnection>("ProjectTracker");
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = "spProject_Update";
				cmd.CommandType = CommandType.StoredProcedure;
				AddUpdateParametersWithValues(cmd, data);

				// Execute the stored procedure and read the results
				rowsAffected = await cmd.ExecuteNonQueryAsync();

				// Check the return value to ensure correct execution in the SP
				returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
				switch (returnValue)
				{
					case 0: break;
					case 1: throw new UnknownDatabaseException();
					case 2: throw new IncorrectRowCountException();
					case 3: throw new ConcurrencyException();
					default: throw new UnknownReturnValueException();
				}

				// Update the DTO with any values that were changed
				UpdateDTOForManagedFields(cmd, data);
			}

			return data;
		}

		/// <summary>
		/// Delete a single item from the data store
		/// </summary>
		public async Task DeleteAsync(int id)
		{
			int returnValue;
			int rowsAffected;
			SqlConnection connection;

			connection = await _connectionManager.GetConnectionAsync<SqlConnection>("ProjectTracker");
			using (SqlCommand cmd = connection.CreateCommand())
			{
				cmd.CommandText = "spProject_Delete";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", id);
				AddReturnParameter(cmd);

				// Execute the stored procedure and read the results
				rowsAffected = await cmd.ExecuteNonQueryAsync();

				// Check the return value to ensure correct execution in the SP
				returnValue = (int)cmd.Parameters["@ReturnValue"].Value;
				switch (returnValue)
				{
					case 0: break;
					case 1: throw new UnknownDatabaseException();
					case 2: throw new IncorrectRowCountException();
					default: throw new UnknownReturnValueException();
				}

			}
		}

		#endregion

		#region Private Helper Methods

		#region Enums

		private enum Fieldset : byte
		{
			All = 0,
			LightweightList = 1
		}

		#endregion

		/// <summary>
		/// Populate a DTO with the data for an item using the provided datareader
		/// </summary>
		/// <param name="dr">The datareader containing the data</param>
		/// <param name="fieldset">The set of fields that must be loaded</param>
		/// <returns>The DTO populated with the data from the store</returns>
		private ProjectDTO LoadDTO(SafeDataReader dr, Fieldset fieldset)
		{
			ProjectDTO data = new ProjectDTO();

			data.Id = dr.GetInt32("Id");
			data.Name = dr.GetString("Name");
			data.Description = dr.GetString("Description");
			data.Started = dr.GetDateTime("Started");
			data.Ended = dr.GetDateTime("Ended");
			data.CreatedAt = dr.GetDateTime("CreatedAt");
			data.CreatedBy = dr.GetString("CreatedBy");
			data.UpdatedAt = dr.GetDateTime("UpdatedAt");
			data.UpdatedBy = dr.GetString("UpdatedBy");
			if (fieldset == Fieldset.All)
			{
			}

			return data;
		}

		/// <summary>
		/// Add the parameters required for inserting an item into the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddInsertParametersWithValues(SqlCommand cmd, ProjectDTO data)
		{
			// Add all of the parameters shared between insert and update operations
			AddSharedInsertUpdateParametersWithValues(cmd, data);
		}

		/// <summary>
		/// Add the parameters required for updating an item in the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddUpdateParametersWithValues(SqlCommand cmd, ProjectDTO data)
		{
			// Add all of the parameters shared between insert and update operations
			AddSharedInsertUpdateParametersWithValues(cmd, data);
		}

		/// <summary>
		/// Add the shared parameters required for inserting and updating an item in the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddSharedInsertUpdateParametersWithValues(SqlCommand cmd, ProjectDTO data)
		{
			cmd.Parameters.AddWithValue("@Id", data.Id);
			cmd.Parameters["@Id"].Direction = ParameterDirection.InputOutput;
			cmd.Parameters.AddWithValue("@Name", data.Name);
			cmd.Parameters.AddWithValue("@Description", data.Description);
			if (data.Started == DateTime.MinValue)
				cmd.Parameters.AddWithValue("@Started", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@Started", data.Started);
			if (data.Started == DateTime.MinValue)
				cmd.Parameters.AddWithValue("@Ended", DBNull.Value);
			else
				cmd.Parameters.AddWithValue("@Ended", data.Ended);
			cmd.Parameters.AddWithValue("@CreatedAt", data.CreatedAt);
			cmd.Parameters["@CreatedAt"].Direction = ParameterDirection.InputOutput;
			cmd.Parameters.AddWithValue("@CreatedBy", data.CreatedBy);
			cmd.Parameters.AddWithValue("@UpdatedAt", data.UpdatedAt);
			cmd.Parameters["@UpdatedAt"].Direction = ParameterDirection.InputOutput;
			cmd.Parameters.AddWithValue("@UpdatedBy", data.UpdatedBy);
			AddReturnParameter(cmd);
		}
		
		/// <summary>
		/// Write changed made by the data store back into the DTO provided
		/// </summary>
		/// <param name="cmd">The SqlCommand whose parameters we are to use as the source of changes</param>
		/// <param name="data">The DTO to be updated with the changes from the store</param>
		private void UpdateDTOForManagedFields(SqlCommand cmd, ProjectDTO data)
		{
			// Update the DTO with data from all parameters that can change on the server
			data.Id = (int)cmd.Parameters["@Id"].Value;
			data.CreatedAt = (DateTime)cmd.Parameters["@CreatedAt"].Value;
			data.UpdatedAt = (DateTime)cmd.Parameters["@UpdatedAt"].Value;
		}

		/// <summary>
		/// Add the return parameter to a command object to use on determining the outcome
		/// of a subsequent call made to the server 
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the parameter definition</param>
		private void AddReturnParameter(SqlCommand cmd)
		{
			cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
			cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
		}

		#endregion

	}
}