using DigitalProject.Common.Filter;
using DigitalProject.Models.User;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DigitalProject.Controllers.Admin
{
    [Route("api/[controller]")]

    [ApiController]
    public class UserManagementController : ControllerBase
    {
        public readonly IUserService _userService;
        private IConfiguration _configuration;
        public UserManagementController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetAllUser")]
        public IActionResult GetAllUser() {
            try
            {
                return Ok(_userService.GetListUser());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            var user = _userService.getByUserId(id);
            if (user == null)
            {
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
                return StatusCode(500, responseData);
            }
        }
     
        [HttpPut("{id}")]
       
        public IActionResult UpdateUser(UserRequestData dto, int id)
        {
            var result = _userService.EditUser(dto, id);
            if (!result)
                return NotFound("Người dùng không tồn tại");

            return Ok("Cập nhật người dùng thành công");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (result == false) return NotFound("Người dùng không tồn tại");
            return Ok("Xóa người dùng thành công");

        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? key ,bool isActive)
        {
            try
            {
                var data = _userService.GetByKeyword(key, isActive);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Lỗi " + ex.Message });
            }

        }
    }
}
   