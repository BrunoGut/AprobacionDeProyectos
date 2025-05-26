using Application.Interfaces.ICommand;
using Application.Exceptions;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class ProjectProposalCommand : IProjectProposalCommand
    {
        private readonly AppDbContext _context;

        public ProjectProposalCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectProposal> CreateProject(ProjectProposal project)
        {
            try
            {
                await _context.AddAsync(project);
                await _context.SaveChangesAsync();
                return project;
            }
            catch (DbUpdateException)
            {
                throw new NotFoundException("No se pudo generar un projecto");
            }
        }

        public async Task UpdateProposalStatusAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Update(proposal);
            await _context.SaveChangesAsync();
        }
    }
}
