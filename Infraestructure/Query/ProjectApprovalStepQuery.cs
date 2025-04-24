using Aplication.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Query
{
    public class ProjectApprovalStepQuery : IProjectApprovalStepQuery
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectApprovalStep>> GetAllStepsAsync()
        {
            return await _context.ProjectApprovalSteps.ToListAsync();
        }

        public async Task<List<ProjectApprovalStep>> GetStepsByUserAsync(int userId)
        {
            return await _context.ProjectApprovalSteps
                .Where(s => s.ApproverUserId == userId)
                .ToListAsync();
        }

        public async Task<List<ProjectApprovalStep>> GetStepsByProposalAsync(Guid proposalId)
        {
            return await _context.ProjectApprovalSteps
                .Where(s => s.ProjectProposalId == proposalId)
                .ToListAsync();
        }

        public async Task<List<ProjectApprovalStep>> GetStepsByRoleAndStatusAsync(int role, int status)
        {
            var pasos = await _context.ProjectApprovalSteps
        .Include(p => p.ProjectProposal)
        .Where(p => p.ApproverRoleId == role && p.Status == status)
        .ToListAsync();

            var pasosFiltrados = new List<ProjectApprovalStep>();

            foreach (var paso in pasos)
            {
                if (paso.StepOrder == 1)
                {
                    // Primer paso, no requiere validación previa
                    pasosFiltrados.Add(paso);
                }
                else
                {
                    var pasoAnterior = await _context.ProjectApprovalSteps
                        .FirstOrDefaultAsync(p =>
                            p.ProjectProposalId == paso.ProjectProposalId &&
                            p.StepOrder == paso.StepOrder - 1);

                    if (pasoAnterior != null && pasoAnterior.Status == 2) // 2 = Aprobado
                    {
                        pasosFiltrados.Add(paso);
                    }
                }
            }

            return pasosFiltrados;
        }

        public async Task<ProjectApprovalStep> GetStepByProposalAndUserAsync(Guid proposalId, int userId)
        {
            return await _context.ProjectApprovalSteps
                .FirstOrDefaultAsync(s => s.ProjectProposalId == proposalId
                                       && s.ApproverUserId == userId
                                       && s.Status == 1); // pendiente
        }

        public async Task<List<ProjectApprovalStep>> GetPendingStepsByUserAsync(int userId)
        {
            return await _context.ProjectApprovalSteps
                .Where(s => s.ApproverUserId == userId && s.Status == 1)
                .ToListAsync();
        }
    }
}
