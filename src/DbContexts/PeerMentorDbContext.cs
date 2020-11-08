namespace Codecool.PeerMentors.DbContexts
{
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class PeerMentorDbContext : IdentityDbContext<User>
    {
        public PeerMentorDbContext(DbContextOptions<PeerMentorDbContext> options)
            : base(options)
        {
        }
    }
}
