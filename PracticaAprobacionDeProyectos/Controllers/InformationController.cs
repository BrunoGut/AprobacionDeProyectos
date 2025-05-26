using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PracticaAprobacionDeProyectos.Controllers
{
    [Route("api")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IProjectTypeService _typeService;
        private readonly IApproverRoleService _roleService;
        private readonly IApprovalStatusService _statusService;
        private readonly IUserService _userService;

        public InformationController(IAreaService areaService, IProjectTypeService typeService, IApproverRoleService roleService, IApprovalStatusService statusService, IUserService userService)
        {
            _areaService = areaService;
            _typeService = typeService;
            _roleService = roleService;
            _statusService = statusService;
            _userService = userService;
        }

        [HttpGet("Area")]
        public async Task<IActionResult> GetAllArea()
        {
            var result = await _areaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ProjectType")]
        public async Task<IActionResult> GetAllProjectType()
        {
            var result = await _typeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("Role")]
        public async Task<IActionResult> GetAllRole()
        {
            var result = await _roleService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("ApprovalStatus")]
        public async Task<IActionResult> GetAllStatus()
        {
            var result = await _statusService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
    }
}
