﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	public interface IEmploymentHistoryRepository
	{
		
		Task<IList<EmploymentHistoryDTO>> FetchListAsync(int personId);

		Task<EmploymentHistoryDTO> FetchAsync(int employmentHistoryId);

		Task<EmploymentHistoryDTO> InsertAsync(EmploymentHistoryDTO data);

		Task<EmploymentHistoryDTO> UpdateAsync(EmploymentHistoryDTO data);

		Task DeleteAsync(int employmentHistoryId);

	}
}