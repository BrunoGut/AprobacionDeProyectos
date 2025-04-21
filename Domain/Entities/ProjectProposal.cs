﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProjectProposal
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public DateTime CreateAt { get; set; }
        //fk's and objects
        public Area ProjectArea { get; set; }
        public int Area {  get; set; }
        public ProjectType ProjectType { get; set; }
        public int Type { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public int CreateBy { get; set; }
        //list of objects
        public IList<ProjectApprovalStep> ProjectApprovalSteps { get; set; }
    }
}
