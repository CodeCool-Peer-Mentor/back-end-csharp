namespace Codecool.PeerMentors.Controllers
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TechnologyDTO = Codecool.PeerMentors.DTOs.Technology;

    public class UserTechnologyController : UserTagController<Technology, UserTechnology>
    {
        public UserTechnologyController(PeerMentorDbContext context, UserManager<User> userManager)
            : base(context, userManager)
        {
        }

        [HttpPost("tags/add-technology-tag")]
        public Task<IActionResult> AddTechnology([FromBody] TechnologyDTO technology)
        {
            return AddTag(technology, (context) => context.Techonologies);
        }

        [HttpPost("tags/remove-technology-tag")]
        public Task<IActionResult> RemoveTechnology([FromBody] TechnologyDTO technology)
        {
            return RemoveTag(technology, (context) => context.UserTechonologies);
        }
    }
}
