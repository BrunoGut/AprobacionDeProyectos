using Aplication.Interfaces;
using Application.Interfaces;
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
    public class AreaQuery : IAreaQuery
    {
        public readonly AppDbContext _context;
        public AreaQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Area>> GetAllAreas() =>
            await _context.Areas.ToListAsync();
        public async Task<Area?> GetById(int areaId)
        {
            return await _context.Areas.FirstOrDefaultAsync(u => u.Id == areaId);
        }
    }
}
