namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.DTOs.Responses;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DiscordUserDTO = Codecool.PeerMentors.DTOs.Requests.DiscordUser;
    using ProjectDTO = Codecool.PeerMentors.DTOs.Project;
    using TechnologyDTO = Codecool.PeerMentors.DTOs.Technology;

    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly PeerMentorDbContext context;
        private readonly UserManager<User> userManager;

        public UserController(PeerMentorDbContext context, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        [HttpGet("get-user-data")]
        public async Task<ProfileSettings> GetSettings()
        {
            User user = await context.Users
                .Include(u => u.Discord)
                .SingleAsync(u => u.Email == User.Identity.Name);
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

            return new ProfileSettings(user)
            {
                AllTechnologyTags = allTechnologies.Select(t => TechnologyDTO.From(t)),
                AllProjectTags = allProjects.Select(p => ProjectDTO.From(p)),
                TechnologyTags = userTechnologies.Select(ut => TechnologyDTO.From(ut.Tag)),
                ProjectTags = userProjects.Select(up => ProjectDTO.From(up.Tag)),
            };
        }

        [HttpPost("save-personal-data")]
        public async Task Update([FromBody] SettingsUser editedUser)
        {
            User user = await context.Users.SingleAsync(u => u.Email == User.Identity.Name);
            user.FirstName = editedUser.FirstName;
            user.LastName = editedUser.LastName;
            user.Country = editedUser.Country;
            user.City = editedUser.City;
            user.Module = editedUser.Module;
            await context.SaveChangesAsync();
        }

        [HttpPost("discord")]
        public async Task AddDiscord([FromBody] DiscordUserDTO discordUser)
        {
            User user = await context.Users.SingleAsync(u => u.Email == User.Identity.Name);
            user.Discord = new Entities.DiscordUser(discordUser, user);
            await context.SaveChangesAsync();
        }

        [HttpGet("get-user-data/{id}")]
        public async Task<IActionResult> GetBy(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            User user = await context.Users
                .Include(u => u.Projects)
                .ThenInclude(up => up.Tag)
                .Include(u => u.Technologies)
                .ThenInclude(ut => ut.Tag)
                .Include(u => u.Discord)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new PublicMentorPage(user));
        }

        [HttpGet("get-user-private-page")]
        public async Task<PrivateUser> GetPrivateInfo()
        {
            User user = await userManager.GetUserAsync(User);
            user.Questions = await context.Questions
                .Include(q => q.Author)
                .Where(q => q.Author.Id == user.Id)
                .ToListAsync();
            user.Answers = await context.Answers
                .Include(a => a.Author)
                .Include(a => a.Question)
                .Where(a => a.Author.Id == user.Id)
                .ToListAsync();
            return new PrivateUser(user);
        }
    }
}
