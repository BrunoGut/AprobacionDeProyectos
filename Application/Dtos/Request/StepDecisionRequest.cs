using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Request
{
    public class StepDecisionRequest
    {
        public string Id { get; set; } = string.Empty;
        public int User { get; set; }
        public int Status { get; set; }
        public string Observation { get; set; } = string.Empty;

        public BigInteger GetStepId() => BigInteger.Parse(Id);
    }
}
