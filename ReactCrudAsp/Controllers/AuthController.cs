using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReactCrudAsp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Deneme deneme=new Deneme();

        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<Deneme> Register(DenemeDto request)
        {
            string passwordHash
                =BCrypt.Net.BCrypt.HashPassword(request.Password);
            deneme.Username = request.Username; 
            deneme.PasswordHash = passwordHash;
            return Ok(deneme);

        }
        [HttpPost("login")]
        public ActionResult<Deneme> Login(DenemeDto request)
        {
            if(deneme.Username != request.Username)
            {
                return BadRequest("User Not Found");

            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, deneme.PasswordHash))
            {
                return BadRequest("Wrong Password");
            }
            string token = CreateToken(deneme);
            return Ok(token);

        }
        private string CreateToken(Deneme deneme)
        {
            List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name,deneme.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
