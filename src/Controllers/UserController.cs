namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.DTOs.Responses;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ProjectDTO = Codecool.PeerMentors.DTOs.Project;
    using TechnologyDTO = Codecool.PeerMentors.DTOs.Technology;

    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public UserController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet("get-user-data")]
        public async Task<ProfileSettings> GetSettings()
        {
            User user = await context.Users.SingleAsync(u => u.Email == User.Identity.Name);
            List<Technology> allTechnologies = await context.Techonologies.ToListAsync();
            List<UserTechnology> userTechnologies = await context.UserTechonologies
                .Include(ut => ut.Tag)
                .Where(ut => ut.User.Id == user.Id)
                .ToListAsync();
            List<Project> allProjects = await context.Projects.ToListAsync();
            List<UserProject> userProjects = await context.UserProjects
                .Include(up => up.Tag)
                .Where(ut => ut.User.Id == user.Id)
                .ToListAsync();

            return new ProfileSettings()
            {
                AllTechnologyTags = allTechnologies.Select(t => TechnologyDTO.From(t)),
                AllProjectTags = allProjects.Select(p => ProjectDTO.From(p)),
                TechnologyTags = userTechnologies.Select(ut => TechnologyDTO.From(ut.Tag)),
                ProjectTags = userProjects.Select(up => ProjectDTO.From(up.Tag)),
            };
        }
    }
}
