using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Query
{
    public class ProjectProposalQuery : IProjectProposalQuery
    {
        private readonly AppDbContext _context;
        public ProjectProposalQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectProposal>> GetAllAsync()
        {
            return await _context.ProjectProposals.ToListAsync();
        }

        public async Task<ProjectProposal> GetByIdAsync(Guid id)
        {
            return await _context.ProjectProposals.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
