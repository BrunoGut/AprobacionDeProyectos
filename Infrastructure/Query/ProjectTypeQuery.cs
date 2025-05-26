using Application.Interfaces.IQuery;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class ProjectTypeQuery : IProjectTypeQuery
    {
        private readonly AppDbContext _context;

        public ProjectTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectType>> GetAllAsync()
        {
            return await _context.ProjectTypes.ToListAsync();
        }

        public async Task<ProjectType?> GetByIdAsync(int typeId)
        {
            return await _context.ProjectTypes.FirstOrDefaultAsync(t => t.Id == typeId);
        }
    }
}
