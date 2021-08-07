﻿using System;
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
	public class CustomPropertyRepository : ICustomPropertyRepository
	{
		private readonly IHttpClientFactory _clientFactory;

		#region Constructors

		public CustomPropertyRepository(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		#endregion

		#region Exposed Methods

		/// <summary>
		/// Get a list of items from the data store
		/// </summary>
		/// <returns>A list of DTOs representing the required data</returns>
		public async Task<IList<CustomPropertyDTO>> FetchListAsync(int personId)
		{
			List<CustomPropertyDTO> list = new List<CustomPropertyDTO>();
			CustomPropertyDTO data;

			await DoSlowDataAccessAsync();

			for (int iteration = 0; iteration < 30; iteration++)
			{
				data = new CustomPropertyDTO();
				data.PersonId = personId;
				data.PropertyName = $"Property {iteration}";
				data.PropertyValue = $"{iteration}";
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
		public async Task<CustomPropertyDTO> FetchAsync(int personId)
		{
			CustomPropertyDTO data;

			await DoSlowDataAccessAsync();

			data = new CustomPropertyDTO()
			{
				PersonId = personId,
				PropertyName = "Property X",
				PropertyValue = "0",
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
		public async Task<CustomPropertyDTO> InsertAsync(CustomPropertyDTO data)
		{
			await DoSlowDataAccessAsync();

			return data;
		}

		/// <summary>
		/// Update an existing item in the data store
		/// </summary>
		/// <param name="data">The data to be written to the store</param>
		/// <returns>The original DTO, including any updates from the data store</returns>
		public async Task<CustomPropertyDTO> UpdateAsync(CustomPropertyDTO data)
		{
			await DoSlowDataAccessAsync();

			return data;
		}

		/// <summary>
		/// Delete a single item from the data store
		/// </summary>
		public async Task DeleteAsync(int personId)
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
			//HttpClient httpClient;

			//// Do some slow data access operation - a call to SQL database for example
			//// I'm using an http call as this can be simulated with no external infrastructure
			//httpClient = _clientFactory.CreateClient("backend");
			//_ = await httpClient.GetAsync("api/DoSomeSlowDataAccess");
			await Task.Delay(6);

		}

		#endregion

	}
}