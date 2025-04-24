using Aplication.Interfaces;
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
    public class ProjectApprovalStepCommand : IProjectApprovalStepCommand
    {
        private readonly AppDbContext _context;

        public ProjectApprovalStepCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveStepsAsync(List<ProjectApprovalStep> steps)
        {
            _context.ProjectApprovalSteps.AddRange(steps);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApprovalStepAsync(ProjectApprovalStep approvalStep)
        {
            var existingStep = await _context.ProjectApprovalSteps
                .FirstOrDefaultAsync(s => s.Id == approvalStep.Id);

            if (existingStep != null)
            {
                // Actualizamos los campos necesarios
                existingStep.Status = approvalStep.Status;
                existingStep.Observations = approvalStep.Observations;

                // Guardamos los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("El paso de aprobación no fue encontrado.");
            }
        }

        public async Task UpdateProposalStatusAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Update(proposal);
            await _context.SaveChangesAsync();
        }

    }
}
