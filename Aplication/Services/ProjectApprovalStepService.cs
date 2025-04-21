using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IProjectApprovalStepCommand _command;

        public ProjectApprovalStepService(IProjectApprovalStepCommand command)
        {
            _command = command;
        }

        public async Task ProcessStreamAsync(Guid proposalId, bool approve)
        {
            await _command.ProcessStreamAsync(proposalId, approve);
        }
    }
}
