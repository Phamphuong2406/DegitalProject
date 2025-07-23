using DigitalProject.Common.Filter;
using DigitalProject.Models.Project;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectManagementController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectManagementController(IProjectService projectService)
        {

            _projectService = projectService;
        }
        [HttpGet]
        public IActionResult GetAllProject()
        {
            try
            {
                return Ok(_projectService.GetListProject());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByProjectId(int id)
        {
            try
            {
                var user = _projectService.GetByProjectId(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateNewProject(ProjectDTO model)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
                _projectService.AddProject(model, currentUserId);
                return Ok("Thêm mới dự án thành công ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPut]
        public IActionResult UpdateProject(ProjectDTO dto, int id) {
            try
            {
                _projectService.EditProject(dto, id);
                return Ok("Cập nhật dự án thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
     
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {

            try
            {
                _projectService.DeleteProject(id);
                return Ok("Xóa dự án thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                return Ok(_projectService.GetListProjectByKeyword(key, structuralEngineer, postingStartDate, postingEndDate, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
