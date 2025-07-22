using DigitalProject.Common.Filter;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryManagementController : ControllerBase
    {
        private IGalleryService _galleryService;
        public GalleryManagementController(IGalleryService galleryService) {
        _galleryService = galleryService;
        }

        [HttpGet]
        public IActionResult GetAllGallery()
        {
            try
            {
                return Ok(_galleryService.GetListgallery());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByGalleryId(int id)
        {
            var user = _galleryService.getBygalleryId(id);

            if (user == null)
            {
                return NotFound("Địa điểm không tồn tại");
            }
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateGellery([FromForm]GalleryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
             
                var result = _galleryService.Addgallery(model, currentUserId);
                if (!result) return BadRequest("Địa điểm đã tồn tại vui lòng nhập lại thông tin!");
                return Ok("Thêm mới địa điểm thành công ");
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateGallery([FromForm] GalleryDTO dto, int id)
        {
            var result = _galleryService.Editgallery(dto, id);
            if (!result)
                return NotFound("Địa điểm không tồn tại");

            return Ok("Cập nhật địa điểm thành công");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGallery(int id)
        {
            var result = _galleryService.Deletegallery(id);
            if (result == false) return NotFound("Địa điểm không tồn tại");
            return Ok("Xóa địa điểm thành công");

        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {

                var data = _galleryService.getListGalleryByKeyword(address,  postingStartDate, postingEndDate);
                int totalRecords = data.Count();
                var pagedData = data
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new PagingModel<Gallery>
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
