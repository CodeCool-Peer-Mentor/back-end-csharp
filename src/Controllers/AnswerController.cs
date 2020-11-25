namespace Codecool.PeerMentors.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using NewAnswerDTO = Codecool.PeerMentors.DTOs.Requests.NewAnswer;

    public class AnswerController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<Entities.User> userManager;

        public AnswerController(PeerMentorDbContext context, UserManager<Entities.User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("answers/add")]
        public async Task<IActionResult> Post([FromBody] NewAnswerDTO answer)
        {
            Entities.Question question = await context.Questions
                .SingleOrDefaultAsync(q => q.ID == answer.QuestionID);
            if (question == null)
            {
                return NotFound();
            }

            Entities.User user = await userManager.GetUserAsync(User);
            context.Add(new Entities.Answer(answer.Body, question, user));
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("answers/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] DTOs.Requests.EditedAnswer dto)
        {
            Entities.Answer answer = await context.Answers
                .Include(q => q.Author)
                .SingleOrDefaultAsync(q => q.ID == id);
            if (answer == null)
            {
                return NotFound();
            }

            Entities.User user = await userManager.GetUserAsync(User);
            if (answer.Author.Id != user.Id)
            {
                return Forbid();
            }

            answer.Body = dto.Body;
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
