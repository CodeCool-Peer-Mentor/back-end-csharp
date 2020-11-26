namespace Codecool.PeerMentors.Controllers
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ReviewDTO = Codecool.PeerMentors.DTOs.Requests.Review;

    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<Entities.User> userManager;

        public ReviewController(PeerMentorDbContext context, UserManager<Entities.User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReviewDTO review)
        {
            Entities.User reviewee = await context.Users
                .SingleOrDefaultAsync(u => u.Id == review.RevieweeID);
            if (reviewee == null)
            {
                return NotFound();
            }

            Entities.User reviewer = await userManager.GetUserAsync(User);
            if (review.RevieweeID == reviewer.Id)
            {
                return StatusCode(405, "Can't review yourself!");
            }

            context.Add(new Entities.Review(review.Numerical, review.Text, reviewer, reviewee));
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
