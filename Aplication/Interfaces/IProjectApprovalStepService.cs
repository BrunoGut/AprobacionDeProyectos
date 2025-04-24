using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectApprovalStepService
    {
        Task<List<ProjectApprovalStep>> GenerateStepsAsync(ProjectProposal proposal);
        Task<List<ProjectApprovalStep>> GetPendingStepsAsync(int userId);
        Task ApproveStepAsync(Guid proposalId, int stepId);
        Task RejectStepAsync(Guid proposalId, int stepId);
        Task ObserveStepAsync(Guid proposalId, int stepId, string comment);
        Task<List<ProjectApprovalStep>> GetStepsForProposalAsync(Guid proposalId);
        Task UpdateStepStatusAsync(BigInteger stepId, string newStatus);
        Task ProcessProposalStepAsync(Guid proposalId, int userId, char decision);

    }
}
