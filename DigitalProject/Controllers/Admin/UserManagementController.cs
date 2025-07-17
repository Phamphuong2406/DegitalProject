using DigitalProject.Models.User;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpGet("{id}")]

        public IActionResult GetByUserId(int id)
        {
            var user = _userService.getByUserId(id);
            if (user == null) {
                return NotFound("Người dùng không tồn tại");
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult CreateUser(UserRequestData requestData)
        {
            var responseData = new ResponseData();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = _userService.CreateUser(requestData);
                if (result == "Người dùng đã tồn tại")
                {
                    responseData.Message = "Tài khoản đã tồn tại!";
                    return BadRequest(responseData);
                }
                responseData.Message = "Tạo tài khoản thành công";

                return Ok(responseData);
            }
            catch (Exception ex)
            {
                responseData.Message = "Đã xảy ra lỗi: " + ex.Message;
                return StatusCode(500,responseData);
            }


        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(UserRequestData dto, int id)
        {
            var success = _userService.EditUser(dto, id);
            if (!success)
                return NotFound("Không tìm thấy người dùng");

            return Ok("Cập nhật người dùng thành công");
        }

    }
}
