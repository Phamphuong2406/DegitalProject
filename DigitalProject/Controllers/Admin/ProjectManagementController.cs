using DigitalProject.Common.Filter;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
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
        public IActionResult SearchByKey(string? key, string? structuralEngineer, DateTime? postingStartDate = null, DateTime? postingEndDate = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                
                var data = _projectService.getListProjectByKeyword(key, structuralEngineer, postingStartDate, postingEndDate);
                int totalRecords = data.Count();
                var pagedData = data
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new PagingModel<Project>
                {
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Data = pagedData
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }

        }
    }
}
