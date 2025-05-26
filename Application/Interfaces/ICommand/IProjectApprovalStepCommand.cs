using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICommand
{
    public interface IProjectApprovalStepCommand
    {
        Task SaveStepsAsync(List<ProjectApprovalStep> steps);
        Task UpdateStepAsync(ProjectApprovalStep step);
    }
}
