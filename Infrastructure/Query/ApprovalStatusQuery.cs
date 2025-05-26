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
    public class ApprovalStatusQuery : IApprovalStatusQuery
    {
        private readonly AppDbContext _context;

        public ApprovalStatusQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalStatus>> GetAllAsync()
        {
            return await _context.ApprovalStatuses.ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ApprovalStatuses.AnyAsync(a => a.Id == id);
        }
    }
}
