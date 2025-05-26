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
    public class ApprovalRuleQuery : IApprovalRuleQuery
    {
        private readonly AppDbContext _context;
        public ApprovalRuleQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalRule>> GetAllAsync() =>
            await _context.ApprovalRules.ToListAsync();
    }
}
