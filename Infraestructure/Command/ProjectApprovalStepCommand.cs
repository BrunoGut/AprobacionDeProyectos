using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Command
{
    public class ProjectApprovalStepCommand : IProjectApprovalStepCommand
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectApprovalStep>> GetStepsByProposalId(Guid proposalId)
        {
            return await _context.ProjectApprovalSteps
                .Where(s => s.ProjectProposalId == proposalId)
                .ToListAsync();
        }

        public async Task AddStepAsync(ProjectApprovalStep step)
        {
            _context.ProjectApprovalSteps.Add(step);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStepAsync(ProjectApprovalStep step)
        {
            _context.ProjectApprovalSteps.Update(step);
            await _context.SaveChangesAsync();
        }

        public async Task ProcessStreamAsync(Guid proposalId, bool approve)
        {
            var steps = await _context.ProjectApprovalSteps
            .Where(s => s.ProjectProposalId == proposalId)
            .ToListAsync();

            if (steps == null || !steps.Any())
                throw new Exception("No hay pasos de aprobación para esta propuesta.");

            foreach (var step in steps)
            {
                step.Status = approve ? 2 : 3; // 2 = Aprobado, 3 = Rechazado
                _context.ProjectApprovalSteps.Update(step);
            }

            await _context.SaveChangesAsync();
        }
    }
}
