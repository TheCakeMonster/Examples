using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// Generated from the built-in Scriban RepositoryInterface template

namespace ProjectTracker.Objects.DataContracts
{
	public interface IAssignmentRepository
	{
		
		Task<IList<AssignmentDTO>> FetchListAsync(int projectId);

		Task<AssignmentDTO> FetchAsync(int id);

		Task<AssignmentDTO> InsertAsync(AssignmentDTO data);

		Task<AssignmentDTO> UpdateAsync(AssignmentDTO data);

		Task DeleteAsync(int id);

	}
}