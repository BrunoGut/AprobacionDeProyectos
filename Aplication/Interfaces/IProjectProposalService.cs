using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IProjectProposalService
    {
        Task<ProjectProposal> CreateWithAssignmentAsync(string title, string description, int areaId, int typeId, decimal amount, int duration, int createdBy);
        //Task CreateWithAssignmentAsync(string title, string description, int areaId, int typeId, decimal amount, int duration, int userId);
    }
}
