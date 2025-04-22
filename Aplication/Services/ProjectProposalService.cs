using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Aplication.Interfaces;


namespace Aplication.Services
{
    public class ProjectProposalService : IProjectProposalService
    {
        private readonly IProjectProposalCommand _proposalCommand;
        public ProjectProposalService(IProjectProposalCommand proposalCommand)
        {
            _proposalCommand = proposalCommand;
        }

        public async Task<ProjectProposal> CreateWithAssignmentAsync(string title, string description, int areaId, int typeId, decimal amount, int duration, int createdBy)
        {
            var proposal = new ProjectProposal
            {
                Title = title,
                Description = description,
                Area = areaId,
                Type = typeId,
                EstimatedAmount = amount,
                EstimatedDuration = duration,
                CreateAt = DateTime.Now,
                CreateBy = createdBy,
                Status = 1
            };

            await _proposalCommand.insertAsync(proposal);
            return proposal;
        }
    }
}
