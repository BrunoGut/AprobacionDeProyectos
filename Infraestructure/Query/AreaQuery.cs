﻿using Aplication.Interfaces;
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
        private readonly AppDbContext _context;
        public AreaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAllAsync()
        {
            return await _context.Areas.ToListAsync();
        }

        public async Task<Area> GetByIdAsync(int id)
        {
            return await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
