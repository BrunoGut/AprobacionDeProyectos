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
    }
}
