using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController(AppDbContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await context.Activities.ToListAsync();
        }

        [HttpGet]
        [Route("{get}")]
        public async Task<ActionResult<Activity>> GetActivity(string id)
        {
            var activitiy =  await context.Activities.FindAsync(id);

            if (activitiy == null)
            {
                return NotFound();
            }

            return activitiy;
        }
    }
}
