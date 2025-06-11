using ProjectTracker.Objects.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTracker.UIControl.UnitTests.Fakes
{
    internal class FakeProjectRepository : IProjectRepository
    {
        public Task DeleteAsync(int id)
        {
            return Task.CompletedTask;
        }

        public Task<ProjectDTO> FetchAsync(int id)
        {
            return Task.FromResult(new ProjectDTO { Id = id });
        }

        public Task<IList<ProjectDTO>> FetchListAsync()
        {
            return Task.FromResult((IList<ProjectDTO>)[]);
        }

        public Task<ProjectDTO> InsertAsync(ProjectDTO data)
        {
            return Task.FromResult(data);
        }

        public Task<ProjectDTO> UpdateAsync(ProjectDTO data)
        {
            return Task.FromResult(data);
        }
    }
}