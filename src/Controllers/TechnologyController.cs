namespace Codecool.PeerMentors.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using DTO = Codecool.PeerMentors.DTOs.Technology;
    using Entity = Codecool.PeerMentors.Entities.Technology;

    [Route("[controller]")]
    public class TechnologyController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<Entities.User> userManager;

        public TechnologyController(PeerMentorDbContext context, UserManager<Entities.User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<DTO> Post([FromBody] DTO dto)
        {
            Entity entity = context.Techonologies.FirstOrDefault(t => t.Name == dto.Name);
            if (entity != null)
            {
                return DTO.From(entity);
            }

            Entities.User author = await userManager.GetUserAsync(User);
            entity = context.Add(Entity.From(dto, author)).Entity;
            await context.SaveChangesAsync();
            return DTO.From(entity);
        }
    }
}
