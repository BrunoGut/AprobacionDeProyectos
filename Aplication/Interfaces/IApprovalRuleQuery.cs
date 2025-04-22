using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IApprovalRuleQuery
    {
        Task<List<ApprovalRule>> GetAllAsync();

    }
}
