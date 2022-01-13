using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Csla.Data;
using VehicleTracker.Objects.DataAccess;

// Generated from the built-in Scriban StoredProcedureRepository template

namespace VehicleTracker.DataAccess
{
	public class VehicleRepository : IVehicleRepository
	{
		
		private readonly ConnectionAddressManager _addressManager;

		#region Constructors

		public VehicleRepository(ConnectionAddressManager addressManager)
		{
			_addressManager = addressManager;
		}

		#endregion

		#region Exposed Methods

		/// <summary>
		/// Get a list of items from the data store
		/// </summary>
		/// <returns>A list of DTOs representing the required data</returns>
		public async Task<IList<VehicleDTO>> FetchListAsync()
		{
			int returnValue;
			IList<VehicleDTO> list = new List<VehicleDTO>();

			// Suppress any ambient transaction, so that this read can happen irrespective of other things
			// This is done to avoid the read of lookup objects needed during validation from being blocked during inserts and updates
			using (TransactionScope ts = TransactionScopeFactory.CreateSuppressedReadCommittedTransaction())
			{
				using (var connection = GetConnection())
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.CommandText = "spVehicleList_Fetch";
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
				}

				// Mark the transaction as complete
				ts.Complete();
			}

			return list;
		}

		/// <summary>
		/// Get a single item from the data store using the unique identifiers provided
		/// </summary>
		/// <returns>A DTO containing the data for the requested item</returns>
		public async Task<VehicleDTO> FetchAsync(int vehicleId)
		{
			int returnValue;
			VehicleDTO data = null;

			// Suppress any ambient transaction, so that this read can happen irrespective of other things
			// This is done to avoid the read of lookup objects from being blocked during inserts and updates
			using (TransactionScope ts = TransactionScopeFactory.CreateSuppressedReadCommittedTransaction())
			{
				using (var connection = GetConnection())
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.CommandText = "spVehicle_Fetch";
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
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
				}

				// Mark the transaction as complete
				ts.Complete();
			}
			return data;
		}

		/// <summary>
		/// Insert a new item into the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<VehicleDTO> InsertAsync(VehicleDTO data)
		{
			int returnValue;
			int rowsAffected;

			using (var connection = GetConnection())
			{
				using (SqlCommand cmd = connection.CreateCommand())
				{
					cmd.CommandText = "spVehicle_Insert";
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
			}

			return data;
		}

		/// <summary>
		/// Update an existing item in the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<VehicleDTO> UpdateAsync(VehicleDTO data)
		{
			int returnValue;
			int rowsAffected;

			using (var connection = GetConnection())
			{
				using (SqlCommand cmd = connection.CreateCommand())
				{
					cmd.CommandText = "spVehicle_Update";
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
			}

			return data;
		}

		/// <summary>
		/// Delete a single item from the data store
		/// </summary>
		public async Task DeleteAsync(int vehicleId)
		{
			int returnValue;
			int rowsAffected;

			using (var connection = GetConnection())
			{
				using (SqlCommand cmd = connection.CreateCommand())
				{
					cmd.CommandText = "spVehicle_Delete";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
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
		private VehicleDTO LoadDTO(SafeDataReader dr, Fieldset fieldset)
		{
			VehicleDTO data = new VehicleDTO();

			data.VehicleId = dr.GetInt32("VehicleId");
			data.NickName = dr.GetString("NickName");
			data.KeyReference = dr.GetString("KeyReference");
			if (fieldset == Fieldset.All)
			{
				data.CreatedAt = dr.GetDateTime("CreatedAt");
				data.CreatedBy = dr.GetString("CreatedBy");
				data.UpdatedAt = dr.GetDateTime("UpdatedAt");
				data.UpdatedBy = dr.GetString("UpdatedBy");
			}

			return data;
		}

		/// <summary>
		/// Add the parameters required for inserting an item into the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddInsertParametersWithValues(SqlCommand cmd, VehicleDTO data)
		{
			// Add all of the parameters shared between insert and update operations
			AddSharedInsertUpdateParametersWithValues(cmd, data);
		}

		/// <summary>
		/// Add the parameters required for updating an item in the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddUpdateParametersWithValues(SqlCommand cmd, VehicleDTO data)
		{
			// Add all of the parameters shared between insert and update operations
			AddSharedInsertUpdateParametersWithValues(cmd, data);
		}

		/// <summary>
		/// Add the shared parameters required for inserting and updating an item in the store
		/// </summary>
		/// <param name="cmd">The SqlCommand to which to add the necessary parameters</param>
		/// <param name="data">The data from which to take data for the parameters</param>
		private void AddSharedInsertUpdateParametersWithValues(SqlCommand cmd, VehicleDTO data)
		{
			cmd.Parameters.AddWithValue("@VehicleId", data.VehicleId);
			cmd.Parameters["@VehicleId"].Direction = ParameterDirection.InputOutput;
			cmd.Parameters.AddWithValue("@NickName", data.NickName);
			cmd.Parameters.AddWithValue("@KeyReference", data.KeyReference);
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
		private void UpdateDTOForManagedFields(SqlCommand cmd, VehicleDTO data)
		{
			// Update the DTO with data from all parameters that can change on the server
			data.VehicleId = (int)cmd.Parameters["@VehicleId"].Value;
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

		/// <summary>
		/// Get a connection manager for use in accessing the data store
		/// ConnectionManager helps manage reuse of connections to avoid promotion
		/// to distributed transactions for situations that do not require it
		/// </summary>
		/// <returns>An instance of a connection manager for accessing the required store</returns>
		private SqlConnection GetConnection()
		{
			SqlConnection connection;

			connection = new SqlConnection(_addressManager.GetDBConnectionString("VehicleTracker"));
			if (connection.State != ConnectionState.Open)
			{
				connection.Open();
			}

			return connection;
		}

		#endregion

	}
}