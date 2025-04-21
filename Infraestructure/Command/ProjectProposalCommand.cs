using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Command
{
    public class ProjectProposalCommand : IProjectProposalCommand
    {
        private readonly AppDbContext _context;
        public ProjectProposalCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task createAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Add(proposal);
            await _context.SaveChangesAsync();
        }
    }
}
