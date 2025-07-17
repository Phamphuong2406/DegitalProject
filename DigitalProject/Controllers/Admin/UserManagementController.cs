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
        [HttpPost]
        public IActionResult CreateUser([FromForm] UserRequestData requestData)
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

                return Ok();
            }
            catch (Exception ex)
            {
                responseData.Message = "Đã xảy ra lỗi: " + ex.Message;
                return StatusCode(500,responseData);
            }


        }

    }
}
