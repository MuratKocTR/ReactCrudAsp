using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactCrudAsp.Models;

namespace ReactCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userDbContext.User.ToListAsync();
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<User> AddUser(User objUser)
        {
            _userDbContext.User.Add(objUser);
            await  _userDbContext.SaveChangesAsync();
            return objUser;
        }

        [HttpPatch]
        [Route("UpdateUser/{id}")]
        public async Task<User> UpdateUser(User objUser)
        {
            _userDbContext.Entry(objUser).State=EntityState.Modified;
            await _userDbContext.SaveChangesAsync();
            return objUser;
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public bool DeleteUser(int id)
        {
            bool a = false;
            var user = _userDbContext.User.Find(id);
            if (user !=null)
            {
                a = true;
                _userDbContext.Entry(user).State = EntityState.Deleted;
                _userDbContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
