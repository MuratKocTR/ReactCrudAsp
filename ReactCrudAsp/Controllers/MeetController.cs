using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactCrudAsp.Models;

namespace ReactCrudAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetController : ControllerBase
    {
        private readonly MeetDbContext _meetDbContext;

        public MeetController(MeetDbContext meetDbContext)
        {
            _meetDbContext = meetDbContext;
        }

        [HttpGet]
        [Route("GetMeets")]
        public async Task<IEnumerable<Meet>> GetMeets()
        {
            return await _meetDbContext.Meet.ToListAsync();
        }

        [HttpPost]
        [Route("AddMeet")]
        public async Task<Meet> AddMeet(Meet objMeet)
        {
            _meetDbContext.Meet.Add(objMeet);
            await _meetDbContext.SaveChangesAsync();
            return objMeet;
        }

        [HttpPatch]
        [Route("UpdateMeet/{id}")]
        public async Task<Meet> UpdateMeet(Meet objMeet)
        {
            _meetDbContext.Entry(objMeet).State = EntityState.Modified;
            await _meetDbContext.SaveChangesAsync();
            return objMeet;
        }

        [HttpDelete]
        [Route("DeleteMeet/{id}")]
        public bool DeleteMeet(int id)
        {
            bool a = false;
            var meet = _meetDbContext.Meet.Find(id);
            if (meet != null)
            {
                a = true;
                _meetDbContext.Entry(meet).State = EntityState.Deleted;
                _meetDbContext.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
