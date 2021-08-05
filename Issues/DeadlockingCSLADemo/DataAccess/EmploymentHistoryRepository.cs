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
	public class EmploymentHistoryRepository : IEmploymentHistoryRepository
	{
		private readonly IHttpClientFactory _clientFactory;

		#region Constructors

		public EmploymentHistoryRepository(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		#endregion

		#region Exposed Methods

		/// <summary>
		/// Get a list of items from the data store
		/// </summary>
		/// <returns>A list of DTOs representing the required data</returns>
		public async Task<IList<EmploymentHistoryDTO>> FetchListAsync(int personId)
		{
			List<EmploymentHistoryDTO> list = new List<EmploymentHistoryDTO>();
			EmploymentHistoryDTO data;

			await DoSlowDataAccessAsync();

			for (int iteration = 0; iteration < 10; iteration++)
			{
				data = new EmploymentHistoryDTO();
				data.PersonId = personId;
				data.EmployerName = $"Company {iteration}";
				data.JoinedOn = DateTime.Now.AddYears(-15 + iteration);
				data.DepartedOn = DateTime.Now.AddYears(-14 + iteration);
				data.CreatedBy = "Joe Smith";
				data.CreatedAt = DateTime.Now.AddHours(-9);
				data.UpdatedBy = "Joe Smith";
				data.UpdatedAt = DateTime.Now.AddHours(-9);
				list.Add(data);
			}

			return list;
		}

		/// <summary>
		/// Get a single item from the data store using the unique identifiers provided
		/// </summary>
		/// <returns>A DTO containing the data for the requested item</returns>
		public async Task<EmploymentHistoryDTO> FetchAsync(int employmentHistoryId)
		{
			EmploymentHistoryDTO data;

			await DoSlowDataAccessAsync();

			data = new EmploymentHistoryDTO()
			{ 
				EmploymentHistoryId = employmentHistoryId, 
				PersonId = 1, 
				EmployerName = "Company X", 
				JoinedOn = DateTime.Now.AddYears(-15), 
				DepartedOn = DateTime.Now.AddYears(-14),
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
		public async Task<EmploymentHistoryDTO> InsertAsync(EmploymentHistoryDTO data)
		{
			await DoSlowDataAccessAsync();

			return data;
		}

		/// <summary>
		/// Update an existing item in the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<EmploymentHistoryDTO> UpdateAsync(EmploymentHistoryDTO data)
		{
			await DoSlowDataAccessAsync();

			return data;
		}

		/// <summary>
		/// Delete a single item from the data store
		/// </summary>
		public async Task DeleteAsync(int employmentHistoryId)
		{
			await DoSlowDataAccessAsync();
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

		private async Task DoSlowDataAccessAsync()
		{
			HttpClient httpClient;

			// Do some slow data access operation - a call to SQL database for example
			// I'm using an http call as this can be simulated with no external infrastructure
			httpClient = _clientFactory.CreateClient("backend");
			_ = await httpClient.GetAsync("api/DoSomeSlowDataAccess");

		}

		#endregion

	}
}