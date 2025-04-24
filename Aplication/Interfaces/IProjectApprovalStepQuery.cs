using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectApprovalStepQuery
    {
        Task<List<ProjectApprovalStep>> GetAllStepsAsync();
        Task<List<ProjectApprovalStep>> GetStepsByUserAsync(int userId);
        Task<List<ProjectApprovalStep>> GetStepsByProposalAsync(Guid proposalId);
        Task<List<ProjectApprovalStep>> GetStepsByRoleAndStatusAsync(int role, int status);
        Task<ProjectApprovalStep> GetStepByProposalAndUserAsync(Guid proposalId, int userId);
        Task<List<ProjectApprovalStep>> GetPendingStepsByUserAsync(int userId);


    }
}

