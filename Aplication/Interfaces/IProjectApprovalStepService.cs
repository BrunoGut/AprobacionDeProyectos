using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectApprovalStepService
    {
        Task<List<ProjectApprovalStep>> GenerateStepsAsync(ProjectProposal proposal);
        //Task GenerateStepsAsync(ProjectProposal proposal);
    }
}
