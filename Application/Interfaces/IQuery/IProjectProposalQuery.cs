using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IQuery
{
    public interface IProjectProposalQuery
    {
        Task<ProjectProposal> GetProjectById(Guid id);
        Task<List<ProjectProposal>> GetAllWithDetailsAsync();
        Task<ProjectProposal?> GetByTitleAsync(string title);


    }
}
