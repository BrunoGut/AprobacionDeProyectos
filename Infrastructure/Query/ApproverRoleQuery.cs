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
    public class ApproverRoleQuery : IApproverRoleQuery
    {
        private readonly AppDbContext _context;

        public ApproverRoleQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApproverRole>> GetAllAsync()
        {
            return await _context.ApproverRoles.ToListAsync();
        }
    }
}
