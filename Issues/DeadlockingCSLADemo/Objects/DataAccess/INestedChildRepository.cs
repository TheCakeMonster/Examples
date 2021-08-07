using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	public interface INestedChildRepository
	{
		
		Task<IList<NestedChildDTO>> FetchListAsync(int personId);

		Task<IList<NestedChildDTO>> FetchNestedListAsync(int parentNestedChildId);

		Task<NestedChildDTO> FetchAsync(int customPropertyId);

		Task<NestedChildDTO> InsertAsync(NestedChildDTO data);

		Task<NestedChildDTO> UpdateAsync(NestedChildDTO data);

		Task DeleteAsync(int customPropertyId);

	}
}