using Aplication.Interfaces;
using Aplication.Exceptions;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ProjectProposal> insertAsync(ProjectProposal projectProposal)
        {
            try
            {
                await _context.ProjectProposals.AddAsync(projectProposal);
                await _context.SaveChangesAsync();
                return projectProposal;
            }
            catch(DbUpdateException)
            {
                throw new ExceptionNotFound("No se pudo registrar la propuesta.");
            }
        }
    }
}
