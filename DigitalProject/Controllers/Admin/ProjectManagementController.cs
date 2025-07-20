using DigitalProject.Common.Filter;
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
        public IActionResult GetByUserId(int id)
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
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _projectService.DeleteProject(id);
            if (result == false) return NotFound("Dự án không tồn tại");
            return Ok("Xóa dự án thành công");

        }
    }
}
