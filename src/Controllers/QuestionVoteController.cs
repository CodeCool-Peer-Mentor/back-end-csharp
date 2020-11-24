namespace Codecool.PeerMentors.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.DTOs.Requests;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class QuestionVoteController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<Entities.User> userManager;

        public QuestionVoteController(PeerMentorDbContext context, UserManager<Entities.User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost("question/vote/{id}")]
        public async Task<IActionResult> Vote(int id, [FromBody] Vote vote)
        {
            if (!(vote.Value == -1 || vote.Value == 1))
            {
                return BadRequest();
            }

            Entities.Question question = await context.Questions
                .Include(q => q.Author)
                .Include(q => q.Votes)
                .ThenInclude(qv => qv.Voter)
                .SingleOrDefaultAsync(q => q.ID == id);
            if (question == null)
            {
                return NotFound();
            }

            Entities.User user = await userManager.GetUserAsync(User);
            if (question.Author.Id == user.Id)
            {
                return StatusCode(405, "Can't vote on your own question!");
            }

            if (Math.Abs(question.Votes.Where(v => v.Voter.Id == user.Id).Sum(v => v.Value)) == 1)
            {
                return StatusCode(405, "You've already voted!");
            }

            context.Add(new Entities.QuestionVote(user, question, vote.Value));
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
