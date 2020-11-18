namespace Codecool.PeerMentors.Controllers
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProjectDTO = Codecool.PeerMentors.DTOs.Project;

    public class UserProjectController : UserTagController<Project, UserProject>
    {
        public UserProjectController(PeerMentorDbContext context, UserManager<User> userManager)
            : base(context, userManager)
        {
        }

        [HttpPost("tags/add-project-tag")]
        public Task<IActionResult> Add([FromBody] ProjectDTO project)
        {
            return AddTag(project, (context) => context.Projects);
        }

        [HttpPost("tags/remove-project-tag")]
        public Task<IActionResult> Remove([FromBody] ProjectDTO project)
        {
            return RemoveTag(project, (context) => context.UserProjects);
        }
    }
}
