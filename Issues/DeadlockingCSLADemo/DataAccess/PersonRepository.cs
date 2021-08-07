using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Csla.Data;
using DeadlockingCSLADemo.Objects.DataAccess;

namespace DeadlockingCSLADemo.DataAccess
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IHttpClientFactory _clientFactory;
		private readonly SQLQueryExecutor _queryExecutor;

		#region Constructors

		public PersonRepository(IHttpClientFactory clientFactory, SQLQueryExecutor queryExecutor)
		{
			_clientFactory = clientFactory;
			_queryExecutor = queryExecutor;
		}

		#endregion

		#region Exposed Methods

		/// <summary>
		/// Get a list of items from the data store
		/// </summary>
		/// <returns>A list of DTOs representing the required data</returns>
		public async Task<IList<PersonDTO>> FetchListAsync()
		{
			await DoSlowDataAccess();

			return new List<PersonDTO>();
		}

		/// <summary>
		/// Get a single item from the data store using the unique identifiers provided
		/// </summary>
		/// <returns>A DTO containing the data for the requested item</returns>
		public async Task<PersonDTO> FetchAsync(int personId)
		{
			PersonDTO data;

			await DoSlowDataAccess();

			data = new PersonDTO()
			{
				PersonId = personId,
				FirstName = "FirstName",
				LastName = "LastName",
				CreatedBy = "Joe Smith",
				CreatedAt = DateTime.Now.AddHours(-9),
				UpdatedBy = "Joe Smith",
				UpdatedAt = DateTime.Now.AddHours(-9)
			};
			return data;
		}

		/// <summary>
		/// Insert a new item into the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<PersonDTO> InsertAsync(PersonDTO data)
		{
			await DoSlowDataAccess();

			return data;
		}

		/// <summary>
		/// Update an existing item in the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<PersonDTO> UpdateAsync(PersonDTO data)
		{
			await DoSlowDataAccess();

			return data;
		}

		/// <summary>
		/// Delete a single item from the data store
		/// </summary>
		public async Task DeleteAsync(int personId)
		{
			await DoSlowDataAccess();
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

		private async Task DoSlowDataAccess()
		{
			//HttpClient httpClient;

			//// Do some slow data access operation - a call to SQL database for example
			//// I'm using an http call as this can be simulated with no external infrastructure
			//httpClient = _clientFactory.CreateClient("backend");
			//_ = await httpClient.GetAsync("api/DoSomeSlowDataAccess");
			// await Task.Delay(20);
			await _queryExecutor.PerformDataAccessAsync();

		}

		#endregion

	}
}