using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        //fk's and objects
        public int Role { get; set; }
        public ApproverRole ApproverRole { get; set; }
        //list of objects
        public IList<ProjectProposal> ProjectProposals { get; set; }
        public IList<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
    }
}
