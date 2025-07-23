using DigitalProject.Models.User;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetByUserId(int id)
        {
            try
            {
                return Ok(_userService.GetByUserId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
        [HttpPost]
        public IActionResult CreateUser(UserRequestData requestData)
        {
            var responseData = new ResponseData();
            try
            {
                _userService.CreateUser(requestData);
                responseData.Message = "Tạo tài khoản thành công";
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
     
        [HttpPut("{id}")]
       
        public IActionResult UpdateUser(UserRequestData dto, int id)
        {
            try
            {
                _userService.EditUser(dto, id);
                return Ok("Cập nhật người dùng thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
          
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("Xóa người dùng thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
          
        }
        [HttpGet]
        [Route("SearchByKey")]
        public IActionResult SearchByKey(string? key ,bool isActive, int pageNumber=1, int pageSize=10)
        {
            try
            {
                var data = _userService.GetByKeyword(key, isActive, pageNumber, pageSize);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message =  ex.Message });
            }

        }
    }
}
   