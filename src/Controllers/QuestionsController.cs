namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using QuestionDTO = Codecool.PeerMentors.DTOs.Responses.Question;

    public class QuestionsController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public QuestionsController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("question")]
        public async Task<IEnumerable<QuestionDTO>> Get()
        {
            List<Entities.Question> questions = await context.Questions
                .Include(q => q.Author)
                .Include(q => q.Technologies)
                .ThenInclude(qt => qt.Technology)
                .OrderByDescending(q => q.InsertedAt)
                .ToListAsync();
            return questions.Select(q => new QuestionDTO(q));
        }
    }
}
