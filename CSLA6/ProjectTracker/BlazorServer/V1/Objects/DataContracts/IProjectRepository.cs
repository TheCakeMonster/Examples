using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

// Generated from the built-in Scriban RepositoryInterface template

namespace ProjectTracker.Objects.DataContracts
{
	public interface IProjectRepository
	{
		
		Task<IList<ProjectDTO>> FetchListAsync();

		Task<ProjectDTO> FetchAsync(int id);

		Task<ProjectDTO> InsertAsync(ProjectDTO data);

		Task<ProjectDTO> UpdateAsync(ProjectDTO data);

		Task DeleteAsync(int id);

	}
}