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
    public class AccountController : ControllerBase
    {
        public readonly IUserService _userService;
        private IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("AccountLogin")]
        public IActionResult UserLogin(AccountLoginRequestData requestData)
        {
            DataReturnedAfterLogin returnData = new DataReturnedAfterLogin();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user_login = _userService.LoginUser(requestData);
            if (user_login == null)
            {
                return BadRequest("Thông tin không hợp lệ!");
            }
            var authClaims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user_login.UserName.ToString()) ,
            new Claim(ClaimTypes.PrimarySid, user_login.UserId.ToString()),
            new Claim(ClaimTypes.GivenName, user_login.Email.ToString())   };

            var newAccessToken = CreateToken(authClaims);
            var token = new JwtSecurityTokenHandler().WriteToken(newAccessToken);
            var refreshToken = GenerateRefreshToken();
            var expired = _configuration["JWT:RefeshTokenValidityInDays"] ?? "";
            _userService.AccountUpdateRefreshToken(new AccountUpdateRefeshTokenRequestData
            {
                Id = user_login.UserId,
                RefreshToken = refreshToken,
                RefreshTokenExprired = DateTime.Now.AddDays(Convert.ToInt32(expired))
            });

            returnData.ResponseCode = 1;
            returnData.ResponseMessage = "Đăng nhập thành công";
            returnData.token = token;
            returnData.refeshToken = refreshToken;
            return Ok(returnData);

        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningkey, SecurityAlgorithms.HmacSha256));
            return token;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
