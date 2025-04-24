using Aplication.Interfaces;
using Aplication.Helpers;
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
        private readonly IProjectApprovalStepQuery _stepQuery;
        private readonly IProjectProposalQuery _projectProposalQuery;
        private readonly IProjectApprovalStepCommand _stepCommand;

        public ProjectApprovalStepService(
            IApprovalRuleQuery ruleQuery,
            IUserQuery userQuery, IProjectApprovalStepQuery stepQuery,
            IProjectProposalQuery projectProposalQuery,
            IProjectApprovalStepCommand stepCommand)
        {
            _ruleQuery = ruleQuery;
            _userQuery = userQuery;
            _stepQuery = stepQuery;
            _projectProposalQuery = projectProposalQuery;
            _stepCommand = stepCommand;
        }

        public async Task<List<ProjectApprovalStep>> GenerateStepsAsync(ProjectProposal proposal)
        {
            var rules = await _ruleQuery.GetAllAsync();
            var users = await _userQuery.GetAllAsync();

            decimal amount = proposal.EstimatedAmount;
            int area = proposal.Area;
            int type = proposal.Type;

            bool InRange(decimal value, decimal min, decimal? max) =>
                value >= min && (max == 0 || value <= max);

            var steps = new List<ProjectApprovalStep>();

            // Agrupar por StepOrder
            var rulesGrouped = rules
                .Where(r => InRange(amount, r.MinAmount, r.MaxAmount))
                .GroupBy(r => r.StepOrder);

            foreach (var group in rulesGrouped)
            {
                var stepOrder = group.Key;

                // Elegir una sola regla por StepOrder, en orden de prioridad
                var selectedRule =
                    group.FirstOrDefault(r => r.Area == area && r.Type == type) ??
                    group.FirstOrDefault(r => r.Area == area && r.Type == null) ??
                    group.FirstOrDefault(r => r.Type == type && r.Area == null) ??
                    group.FirstOrDefault(r => r.Area == null && r.Type == null);

                if (selectedRule == null)
                    continue; // No hay regla válida para este StepOrder

                var approvers = users
                    .Where(u => u.Role == selectedRule.ApproverRoleId)
                    .ToList();

                if (approvers.Any())
                {
                    foreach (var user in approvers)
                    {
                        steps.Add(new ProjectApprovalStep
                        {
                            Id = GenerateRandomBigIntId(),
                            ProjectProposalId = proposal.Id,
                            ApproverRoleId = selectedRule.ApproverRoleId,
                            ApproverUserId = user.Id,
                            Status = 1,
                            StepOrder = selectedRule.StepOrder,
                            Observations = "Pendiente"
                        });
                    }
                }
                else
                {
                    steps.Add(new ProjectApprovalStep
                    {
                        Id = GenerateRandomBigIntId(),
                        ProjectProposalId = proposal.Id,
                        ApproverRoleId = selectedRule.ApproverRoleId,
                        ApproverUserId = null,
                        Status = 1,
                        StepOrder = selectedRule.StepOrder,
                        Observations = "Pendiente (sin usuario asignado)"
                    });
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

        // Obtener los pasos pendientes de un usuario
        public async Task<List<ProjectApprovalStep>> GetPendingStepsAsync(int userId)
        {
            var steps = await _stepQuery.GetStepsByUserAsync(userId);
            return steps.Where(s => s.Status == 1).ToList(); // Solo los pasos pendientes
        }

        // Aprobar un paso de la propuesta
        public async Task ApproveStepAsync(Guid proposalId, int stepId)
        {
            var steps = await _stepQuery.GetStepsByProposalAsync(proposalId);
            var approvalStep = steps.FirstOrDefault(s => s.Id == stepId);

            if (approvalStep == null || approvalStep.Status != 1)
            {
                throw new InvalidOperationException("El paso no es válido para aprobar.");
            }

            approvalStep.Status = 2;
            var allStepsApproved = steps.All(s => s.Status == 2);
            if (allStepsApproved)
            {
                var proposal = await _projectProposalQuery.GetByIdAsync(proposalId);
                proposal.Status = ApprovalStatusHelper.Aprobado;
            }

            await _stepCommand.UpdateApprovalStepAsync(approvalStep);
        }

        // Rechazar un paso de la propuesta
        public async Task RejectStepAsync(Guid proposalId, int stepId)
        {
            var steps = await _stepQuery.GetStepsByProposalAsync(proposalId);
            var approvalStep = steps.FirstOrDefault(s => s.Id == stepId);

            if (approvalStep == null || approvalStep.Status != 1) // 1: Pendiente
            {
                throw new InvalidOperationException("El paso no es válido para rechazar.");
            }

            approvalStep.Status = 3;
            var proposal = await _projectProposalQuery.GetByIdAsync(proposalId);
            proposal.Status = ApprovalStatusHelper.Rechazado;

            await _stepCommand.UpdateApprovalStepAsync(approvalStep);
        }

        // Agregar observaciones a un paso de la propuesta
        public async Task ObserveStepAsync(Guid proposalId, int stepId, string comment)
        {
            var steps = await _stepQuery.GetStepsByProposalAsync(proposalId);
            var approvalStep = steps.FirstOrDefault(s => s.Id == stepId);

            if (approvalStep == null || approvalStep.Status != 1)
            {
                throw new InvalidOperationException("El paso no es válido para observar.");
            }

            approvalStep.Observations = comment;

            await _stepCommand.UpdateApprovalStepAsync(approvalStep);
        }

        // Obtener todos los pasos de una propuesta por ID (ordenados por StepOrder)
        public async Task<List<ProjectApprovalStep>> GetStepsForProposalAsync(Guid proposalId)
        {
            var steps = await _stepQuery.GetStepsByProposalAsync(proposalId);
            return steps
                .Where(s => s.ProjectProposalId == proposalId)
                .OrderBy(s => s.StepOrder)
                .ToList();
        }


        // Actualizar el estado de un paso específico (por ID)
        public async Task UpdateStepStatusAsync(BigInteger stepId, string newStatus)
        {
            var steps = await _stepQuery.GetAllStepsAsync();
            var step = steps.FirstOrDefault(s => s.Id == stepId);

            if (step == null)
                throw new InvalidOperationException("Paso de aprobación no encontrado.");

            // Opcional: Validación de valores válidos
            if (!ApprovalStatusHelper.IsValidStatus(newStatus))
                throw new ArgumentException("Estado inválido.");

            step.Status = ApprovalStatusHelper.FromString(newStatus);
            await _stepCommand.UpdateApprovalStepAsync(step);
        }

        public async Task ProcessProposalStepAsync(Guid proposalId, int userId, char decision)
        {
            // Obtener el paso correspondiente al usuario y propuesta
            var step = await _stepQuery.GetStepByProposalAndUserAsync(proposalId, userId);

            if (step == null || step.Status != 1) // 1 = pendiente
                throw new Exception("No tiene permisos para aprobar esta propuesta o ya fue procesada.");

            switch (char.ToUpper(decision))
            {
                case 'A':
                    step.Status = 2; // Aprobado
                    break;
                case 'R':
                    step.Status = 3; // Rechazado
                    break;
                case 'O':
                    step.Status = 4; // Observado
                    break;
                default:
                    throw new ArgumentException("Opción inválida.");
            }

            step.DecisionDate = DateTime.Now;
            await _stepCommand.UpdateApprovalStepAsync(step);
        }

    }
}
