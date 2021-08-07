using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeadlockingCSLADemo.Objects.DataAccess
{
	public interface IPersonRepository
	{
		
		Task<IList<PersonDTO>> FetchListAsync();

		Task<PersonDTO> FetchAsync(int personId);

		Task<PersonDTO> InsertAsync(PersonDTO data);

		Task<PersonDTO> UpdateAsync(PersonDTO data);

		Task DeleteAsync(int personId);

	}
}