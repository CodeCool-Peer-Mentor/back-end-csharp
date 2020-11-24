namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<Entities.User> userManager;

        public QuestionController(PeerMentorDbContext context, UserManager<Entities.User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOs.Requests.NewQuestion dto)
        {
            Entities.User author = await userManager.GetUserAsync(User);
            Entities.Question question = new Entities.Question(dto, author);
            List<Entities.Technology> technologies = new List<Entities.Technology>();
            foreach (string technologyName in dto.Technologies)
            {
                Entities.Technology dbTechnology = context.Techonologies
                    .SingleOrDefault(t => t.Name == technologyName);
                if (dbTechnology == null)
                {
                    return NotFound($"Technology with name {technologyName} does not exist!");
                }

                technologies.Add(dbTechnology);
            }

            context.Add(question);
            context.AddRange(technologies.Select(
                t => new Entities.QuestionTechnology(question, t)));
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
