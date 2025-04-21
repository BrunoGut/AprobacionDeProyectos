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
    public class ProjectTypeQuery : IProjectTypeQuery
    {
        private readonly AppDbContext _context;
        public ProjectTypeQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProjectType>> GetAllTypes() =>
            await _context.ProjectTypes.ToListAsync();
        public async Task<ProjectType?> GetById(int typeId)
        {
            return await _context.ProjectTypes.FirstOrDefaultAsync(p => p.Id == typeId);
        }
    }
}
