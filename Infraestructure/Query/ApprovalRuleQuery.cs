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
    public class ApprovalRuleQuery : IApprovalRuleQuery
    {
        private readonly AppDbContext _context;

        public ApprovalRuleQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalRule>> GetAllAsync()
        {
            return await _context.ApprovalRules.ToListAsync();
        }
    }
}
