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
using Application.Dtos.Request;
using Application.Dtos.Response;
using Azure.Core;

namespace Infrastructure.Command
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
            try
            {
                await _context.ProjectApprovalSteps.AddRangeAsync(steps);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new NotFoundException("No se pudieron guardar los pasos de aprobación.");
            }
        }

        public async Task UpdateStepAsync(ProjectApprovalStep step)
        {
            _context.ProjectApprovalSteps.Update(step);
            await _context.SaveChangesAsync();
        }
    }
}
