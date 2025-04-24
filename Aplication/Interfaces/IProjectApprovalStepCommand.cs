using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public  interface IProjectApprovalStepCommand
    {
        Task SaveStepsAsync(List<ProjectApprovalStep> steps);
        Task UpdateApprovalStepAsync(ProjectApprovalStep approvalStep);
        Task UpdateProposalStatusAsync(ProjectProposal proposal);

    }
}
