using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Response
{
    public class ProjectProposalResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal EstimatedAmount { get; set; }
        public int EstimatedDuration { get; set; }
        public int AreaId { get; set; }
        public int TypeId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProjectApprovalStepResponse> Steps { get; set; }
    }
}
