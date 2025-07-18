using DigitalProject.Models.User.Project;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagementController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectManagementController(IProjectService projectService) {
        
        _projectService = projectService;
        }
        [HttpPost]
        public IActionResult CreateNewProject(ProjectDTO model) {

            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
                var result = _projectService.AddProject(model);
                if(!result) return BadRequest("Dự án đã tồn tại vui lòng nhập lại thông tin!");
                return Ok("Thêm mới dự án thành công ");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
