namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.DTOs.Responses;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TechnologyDTO = Codecool.PeerMentors.DTOs.Technology;
    using ProjectDTO = Codecool.PeerMentors.DTOs.Project;

    public class UserController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<User> userManager;

        public UserController(PeerMentorDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet("user/get-user-data")]
        public async Task<ProfileSettings> GetSettings()
        {
            User user = await context.Users.SingleAsync(u => u.Email == User.Identity.Name);
            List<Technology> allTechnologies = await context.Techonologies.ToListAsync();
            List<UserTechnology> userTechnologies = await context.UserTechonologies
                .Include(ut => ut.Technology)
                .Where(ut => ut.User.Id == user.Id)
                .ToListAsync();
            List<Project> allProjects = await context.Projects.ToListAsync();
            List<UserProject> userProjects = await context.UserProjects
                .Include(up => up.Project)
                .Where(ut => ut.User.Id == user.Id)
                .ToListAsync();

            return new ProfileSettings()
            {
                AllTechnologyTags = allTechnologies.Select(t => TechnologyDTO.From(t)),
                AllProjectTags = allProjects.Select(p => ProjectDTO.From(p)),
                TechnologyTags = userTechnologies.Select(ut => TechnologyDTO.From(ut.Technology)),
                ProjectTags = userProjects.Select(up => ProjectDTO.From(up.Project)),
            };
        }

        [HttpPost("tags/add-technology-tag")]
        public async Task<IActionResult> AddTechnology([FromBody] TechnologyDTO technology)
        {
            Technology dbTechnology = await context.Techonologies
                .SingleOrDefaultAsync(t => t.Name == technology.Name);
            if (dbTechnology == null)
            {
                return BadRequest();
            }

            User user = await userManager.GetUserAsync(User);
            UserTechnology userTechnology = await context.UserTechonologies
                .SingleOrDefaultAsync(ut => ut.User.Id == user.Id
                    && ut.Technology.Name == technology.Name);
            if (userTechnology == null)
            {
                context.Add(new UserTechnology(user, dbTechnology));
                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("tags/remove-technology-tag")]
        public async Task<IActionResult> RemoveTechnology([FromBody] TechnologyDTO technology)
        {
            User user = await userManager.GetUserAsync(User);
            UserTechnology userTechnology = await context.UserTechonologies
                .SingleOrDefaultAsync(ut => ut.User.Id == user.Id
                    && ut.Technology.Name == technology.Name);
            if (userTechnology == null)
            {
                return BadRequest();
            }

            context.Remove(userTechnology);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("tags/add-project-tag")]
        public async Task<IActionResult> AddProject([FromBody] ProjectDTO project)
        {
            Project dbProject = await context.Projects
                .SingleOrDefaultAsync(t => t.Name == project.Name);
            if (dbProject == null)
            {
                return BadRequest();
            }

            User user = await userManager.GetUserAsync(User);
            UserProject userProject = await context.UserProjects
                .SingleOrDefaultAsync(up => up.User.Id == user.Id
                    && up.Project.Name == project.Name);
            if (userProject == null)
            {
                context.Add(new UserProject(user, dbProject));
                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost("tags/remove-project-tag")]
        public async Task<IActionResult> RemoveProject([FromBody] ProjectDTO project)
        {
            User user = await userManager.GetUserAsync(User);
            UserProject userProject = await context.UserProjects
                .SingleOrDefaultAsync(up => up.User.Id == user.Id
                    && up.Project.Name == project.Name);
            if (userProject == null)
            {
                return BadRequest();
            }

            context.Remove(userProject);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
