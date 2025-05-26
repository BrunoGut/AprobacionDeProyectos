using Application.Dtos.Request;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PracticaAprobacionDeProyectos.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IPorjectProposalService _proposalService;
        private readonly IProjectApprovalStepService _approvalStepService;

        public ProjectController(IPorjectProposalService proposalService, IProjectApprovalStepService approvalStepService)
        {
            _proposalService = proposalService;
            _approvalStepService = approvalStepService;
        }

        [HttpGet("Project")]
        public async Task<IActionResult> GetProjects(
        [FromQuery] string? title,
        [FromQuery] int? status,
        [FromQuery] int? applicant,
        [FromQuery] int? approvalUser)
        {
            var result = await _proposalService.GetFilteredProjectsAsync(title, status, applicant, approvalUser);
            return Ok(result);
        }

        [HttpPost("Project")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectProposalRequest request)
        {
            var result = await _proposalService.CreateProjectAsync(request);
            return StatusCode(201, result);
        }

        [HttpPatch("Project/{id}/decision")]
        public async Task<IActionResult> PatchDecision(Guid id, [FromBody] StepDecisionRequest request)
        {
            var updated = await _approvalStepService.ProcessStepDecisionAsync(id, request);
            return Ok(updated);
        }

        [HttpPatch("Project/{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] UpdateProjectProposalRequest request)
        {
            var result = await _proposalService.UpdateProposalAsync(id, request);
            return Ok(result);
        }

        [HttpGet("Project/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _proposalService.GetProposalDetailByIdAsync(id);
            return Ok(result);
        }
    }
}
