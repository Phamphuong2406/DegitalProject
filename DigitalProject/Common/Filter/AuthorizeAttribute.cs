using DigitalProject.Entitys;
using DigitalProject.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace DigitalProject.Common.Filter
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute() : base(typeof(DemoAuthorizeActionFilter))
        {

        }
    }

    public class DemoAuthorizeActionFilter : IAsyncAuthorizationFilter
    {

        private IConfiguration _configuration;
        private readonly IUserService _accountService;
        public DemoAuthorizeActionFilter(IUserService accountService, IConfiguration configuration)
        {
            _configuration = configuration;
            _accountService = accountService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //Bước 1: Trích xuất thông tin từ token
            var identity = context.HttpContext.User.Identity as ClaimsIdentity;//Xác định danh tính của người dùng dựa trên các claim trong token.
            if (identity != null)
            {
                var userClaims = identity.Claims;
                var user = new User
                {
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    UserId = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value)

                };
                //Bước 2:xác nhận tính hợp lệ của người dùng
                if (user.UserId <= 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new JsonResult(new
                    {
                        Code = HttpStatusCode.Unauthorized,
                        Message = "Vui lòng đăng nhập để thực hiển chức năng này "
                    });
                    return;
                }
               
            }

        }
    }

}
