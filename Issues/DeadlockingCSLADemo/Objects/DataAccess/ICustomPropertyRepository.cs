﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	public interface ICustomPropertyRepository
	{
		
		Task<IList<CustomPropertyDTO>> FetchListAsync(int personId);

		Task<CustomPropertyDTO> FetchAsync(int customPropertyId);

		Task<CustomPropertyDTO> InsertAsync(CustomPropertyDTO data);

		Task<CustomPropertyDTO> UpdateAsync(CustomPropertyDTO data);

		Task DeleteAsync(int customPropertyId);

	}
}