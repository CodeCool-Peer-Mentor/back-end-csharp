namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DTO = Codecool.PeerMentors.DTOs.Technology;
    using Entity = Codecool.PeerMentors.Entities.Technology;

    [Route("[controller]")]
    public class TechnologiesController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public TechnologiesController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<DTO>> Get()
        {
            List<Entity> technologies = await context.Techonologies
                .OrderBy(t => t.Name)
                .ToListAsync();
            return technologies.Select(t => DTO.From(t));
        }
    }
}
