using Aplication.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class ProjectApprovalStepService : IProjectApprovalStepService
    {
        private readonly IApprovalRuleQuery _ruleQuery;
        private readonly IUserQuery _userQuery;
        private readonly IProjectApprovalStepCommand _stepCommand;

        public ProjectApprovalStepService(
            IApprovalRuleQuery ruleQuery,
            IUserQuery userQuery,
            IProjectApprovalStepCommand stepCommand)
        {
            _ruleQuery = ruleQuery;
            _userQuery = userQuery;
            _stepCommand = stepCommand;
        }

        public async Task<List<ProjectApprovalStep>> GenerateStepsAsync(ProjectProposal proposal)
        {
            var rules = await _ruleQuery.GetAllAsync();
            var users = await _userQuery.GetAllAsync();

            decimal amount = proposal.EstimatedAmount;
            int area = proposal.Area;
            int type = proposal.Type;

            bool InRange(decimal value, decimal min, decimal max) =>
                value >= min && (max <= 0 || value <= max);

            var steps = new List<ProjectApprovalStep>();

            // Agrupar reglas por StepOrder
            var rulesGrouped = rules
                .Where(r => InRange(amount, r.MinAmount, r.MaxAmount))
                .GroupBy(r => r.StepOrder);

            foreach (var group in rulesGrouped)
            {
                var stepOrder = group.Key;

                // 1. Intentar encontrar reglas que matchean area y type
                var exactMatches = group
                    .Where(r => r.Area == area && r.Type == type)
                    .ToList();

                // 2. Si no hay exactas, buscar reglas que matchean solo area o solo type
                var semiMatches = exactMatches.Any() ? new List<ApprovalRule>() :
                    group.Where(r =>
                        (r.Area == area && r.Type == null) ||
                        (r.Type == type && r.Area == null)
                    ).ToList();

                // 3. Si no hay semi-matches, buscar reglas totalmente generales
                var fallback = (exactMatches.Any() || semiMatches.Any()) ? new List<ApprovalRule>() :
                    group.Where(r => r.Area == null && r.Type == null).ToList();

                // Combinar en orden de prioridad
                var selectedRules = exactMatches.Any() ? exactMatches :
                                    semiMatches.Any() ? semiMatches :
                                    fallback;

                foreach (var rule in selectedRules)
                {
                    var approvers = users
                        .Where(u => u.Role == rule.ApproverRoleId)
                        .ToList();

                    foreach (var user in approvers)
                    {
                        steps.Add(new ProjectApprovalStep
                        {
                            Id = GenerateRandomBigIntId(),
                            ProjectProposalId = proposal.Id,
                            ApproverRoleId = rule.ApproverRoleId,
                            ApproverUserId = user.Id,
                            Status = 1,
                            StepOrder = rule.StepOrder,
                            Observations = "Pendiente"
                        });
                    }
                }
            }

            await _stepCommand.SaveStepsAsync(steps);
            return steps.OrderBy(s => s.StepOrder).ToList();
        }


        private static BigInteger GenerateRandomBigIntId()
        {
            var random = new Random();
            byte[] bytes = new byte[16];
            random.NextBytes(bytes);
            return new BigInteger(bytes);
        }
    }
}
