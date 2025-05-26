using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICommand
{
    public interface IProjectProposalCommand
    {
        Task<ProjectProposal> CreateProject(ProjectProposal Project);
        //Task<ProjectProposal> UpdateProject(ProjectProposal Project);
        Task UpdateProposalStatusAsync(ProjectProposal proposal);
        //Task<bool> DeleteProject(Guid projectId);
        //Task<ProjectProposal> FinalizeProject(Guid projectId);
        //Task<ProjectProposal> UpdateProjectStatus(Guid projectId, string decision);
    }
}
