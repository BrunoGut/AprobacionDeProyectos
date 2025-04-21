using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApproverRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //list of objetcs
        public IList<User> Users { get; set; }
        public IList<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
        public IList<ApprovalRule> ApprovalRules { get; set; }
    }
}
