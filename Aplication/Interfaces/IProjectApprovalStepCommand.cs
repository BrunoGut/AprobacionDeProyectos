using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectApprovalStepCommand
    {
        Task<List<ProjectApprovalStep>> GetStepsByProposalId(Guid proposalId);
        Task AddStepAsync(ProjectApprovalStep step);
        Task UpdateStepAsync(ProjectApprovalStep step);
        Task ProcessStreamAsync(Guid proposalId, bool approve);
    }
}
