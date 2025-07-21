using DigitalProject.Common.Filter;
using DigitalProject.Models.User;
using DigitalProject.Models.User.Project;
using DigitalProject.Services.Implements;
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
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByProjectId(int id)
        {
            var user = _projectService.getByProjectId(id);
            if (user == null)
            {
                return NotFound("Dự án không tồn tại");
            }
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateNewProject(ProjectDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
                var result = _projectService.AddProject(model, currentUserId);
                if (!result) return BadRequest("Dự án đã tồn tại vui lòng nhập lại thông tin!");
                return Ok("Thêm mới dự án thành công ");
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPut]
        public IActionResult UpdateProject(ProjectDTO dto, int id) {
            var result = _projectService.EditProject(dto, id);
            if (!result)
                return NotFound("Dự án không tồn tại");

            return Ok("Cập nhật dự án thành công");

        }
     
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var result = _projectService.DeleteProject(id);
            if (result == false) return NotFound("Dự án không tồn tại");
            return Ok("Xóa dự án thành công");

        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null)
        {
            try
            {
                
                var data = _projectService.getListProjectByKeyword(key, structuralEngineer, postingStartDate, postingEndDate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }

        }
    }
}
