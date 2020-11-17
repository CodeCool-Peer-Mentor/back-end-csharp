namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DTO = Codecool.PeerMentors.DTOs.Project;
    using Entity = Codecool.PeerMentors.Entities.Project;

    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public ProjectsController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<DTO>> Get()
        {
            List<Entity> technologies = await context.Projects
                .OrderBy(p => p.Name)
                .ToListAsync();
            return technologies.Select(t => DTO.From(t));
        }
    }
}
