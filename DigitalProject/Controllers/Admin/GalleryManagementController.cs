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
                return Ok(_galleryService.GetListGallery());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByGalleryId(int id)
        {
            try
            {
                var user = _galleryService.GetByGalleryId(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
           
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateGellery([FromForm]GalleryDTO model)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
               _galleryService.AddGallery(model, currentUserId);
                return Ok("Thêm mới địa điểm thành công ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateGallery([FromForm] GalleryDTO dto, int id)
        {
            try
            {
                _galleryService.EditGallery(dto, id);
                return Ok("Cập nhật địa điểm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
         

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGallery(int id)
        {
            try
            {
                _galleryService.DeleteGallery(id);
                return Ok("Xóa địa điểm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
               return Ok(_galleryService.GetListGalleryByKeyword(address, postingStartDate, postingEndDate, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message =  ex.Message });
            }

        }


    }
}
