using DigitalProject.Common.Filter;
using DigitalProject.Models.ContactRequest;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestManagementController : ControllerBase
    {
        private IContactRequestService _requestService;
        public ContactRequestManagementController(IContactRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public IActionResult GetAllContactRequest()
        {
            try
            {
                return Ok(_requestService.GetListContactRequest());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByContactRequestId(int id)
        {
            try
            {
                var user = _requestService.GetByContactRequestId(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPost]
        public IActionResult CreateContactRequest( CreateRequestDTO model)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
                string idAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                _requestService.AddContactRequest(model, currentUserId,idAddress );
                return Ok("Thêm mới yêu cầu thành công ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPut("{id}")]
        public IActionResult UpdateContactRequest(UpdateRequestDTO dto, int id)
        {
            try
            {
                _requestService.UpdateContactRequest(dto, id);
                return Ok("Cập nhật yêu cầu thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPut]
        [Route("UpdateResponseStatus/{id}")]
        [Authorize]
        public IActionResult UpdateResponseStatus(UpdateResponseStatusDTO dto, int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                int currentUserId = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.PrimarySid)?.Value);
                _requestService.UpdateResponseStatus(dto, id, currentUserId);
                return Ok("Cập nhật yêu cầu thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContactRequest(int id)
        {
            try
            {
                _requestService.DeleteContactRequest(id);
                return Ok("Xóa địa điểm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? key, DateTime? requestDate, string? status, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                return Ok(_requestService.GetListContactRequestByKeyword(key, requestDate, status, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
