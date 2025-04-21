using Aplication.Dtos;
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
        Task CreateRequestAsync();
        Task<List<User>> GetAllUsersAsync();
        Task<List<Area>> GetAllAreasAsync();
        Task<List<ProjectType>> GetAllTypesAsync();
    }
}
