﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //list of objects
        public IList<ApprovalRule> ApprovalRules { get; set; }
        public IList<ProjectProposal> ProjectProposals { get; set; }
    }
}
