namespace Codecool.PeerMentors.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DbContexts;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.DTOs.Responses;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class MentorsController : ControllerBase
    {
        private readonly PeerMentorDbContext context;

        public MentorsController(PeerMentorDbContext context)
        {
            this.context = context;
        }

        [HttpGet("filter/get-mentors")]
        public async Task<IEnumerable<Mentor>> All()
        {
            List<User> mentors = await context.Users
                .Include(u => u.Technologies)
                .ThenInclude(ut => ut.Tag)
                .Where(u => u.Projects.Count() > 0 || u.Technologies.Count() > 0)
                .OrderBy(u => u.FirstName)
                .ThenBy(u => u.LastName)
                .ToListAsync();
            return mentors.Select(m => new Mentor(m));
        }

        [HttpPost("filter/get-mentors-by-tags")]
        public async Task<IEnumerable<Mentor>> FilteredBy([FromBody] MentorSearchQuery query)
        {
            IQueryable<User> mentorsQuery = context.Users
                .Include(u => u.Projects)
                .ThenInclude(up => up.Tag)
                .Include(u => u.Technologies)
                .ThenInclude(ut => ut.Tag)
                .Where(u => u.Projects.Count() > 0 || u.Technologies.Count() > 0);
            List<User> mentors = await mentorsQuery.ToListAsync();
            if (query.Projects.Count > 0)
            {
                mentors = mentors.Where(u => HasAllProjects(u, query.Projects))
                    .ToList();
            }

            if (query.Technologies.Count > 0)
            {
                mentors = mentors.Where(u => HasAllTechnologies(u, query.Technologies))
                    .ToList();
            }

            return mentors.Select(m => new Mentor(m));
        }

        private bool HasAllProjects(User user, List<DTOs.Project> requirements)
        {
            return requirements.All(p => user.Projects.Select(up => up.Tag.ID).Contains(p.ID));
        }

        private bool HasAllTechnologies(User user, List<DTOs.Technology> requirements)
        {
            return requirements.All(p => user.Technologies.Select(ut => ut.Tag.ID).Contains(p.ID));
        }
    }
}
