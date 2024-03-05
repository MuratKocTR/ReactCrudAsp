using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
//using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ReactCrudAsp.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        public static User user = new User();

        private readonly IConfiguration _configuration;
        public UserAuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public string Register(User user)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("UserDbContext").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [dbo].[User] WHERE email= '" + user.email + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return "Email";

            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[User](email,password,name,surname,phonenumber) VALUES('" + user.email + "','" + user.password + "','" + user.name + "','" + user.surname + "','" + user.phonenumber + "')", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return "Success";

                }
                else
                {
                    return "Failed";
                }
            }
            
        }
            [HttpPost]
        [Route("login")]
        public string Login(User user)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("UserDbContext").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [dbo].[User] WHERE email= '" + user.email+"' AND password = '"+user.password+"'",con);
            DataTable dt=new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>0)
            {
                string token = CreateToken(user);
                return token;
                
            }
            else
            {
                return "Invalid";
            }
            
            //if (user.email != request.email)
            //{
            //    return BadRequest("User Not Found");

            //}
            //if (!BCrypt.Net.BCrypt.Verify(request.password, user.password))
            //{
            //    return BadRequest("Wrong Password");
            //}
        }
        private string CreateToken(User user)
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Name,user.name),
                new Claim(JwtRegisteredClaimNames.Email,user.email)

        };
            //List<Claim> claims = new List<Claim> {
            //new Claim(ClaimTypes.Name,user.name),
            //new Claim(ClaimTypes.Email,user.email),
            //new Claim(ClaimTypes.UserData,user.password)
            //};
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
