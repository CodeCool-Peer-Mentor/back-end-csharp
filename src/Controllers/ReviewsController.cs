namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ReviewDTO = Codecool.PeerMentors.DTOs.Responses.Review;

    public class ReviewsController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public ReviewsController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet("review/id/{revieweeID}")]
        public async Task<IEnumerable<ReviewDTO>> ForUser(string revieweeID)
        {
            List<Review> reviews = await context.Reviews
                .Include(r => r.Reviewer)
                .Where(r => r.Reviewee.Id == revieweeID)
                .ToListAsync();
            return reviews.Select(r => new ReviewDTO(r));
        }
    }
}
