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
    }
}
