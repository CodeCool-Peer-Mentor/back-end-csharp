namespace Codecool.PeerMentors.DbContexts
{
    using Microsoft.EntityFrameworkCore;

    public class PeerMentorDbContext : DbContext
    {
        public PeerMentorDbContext(DbContextOptions<PeerMentorDbContext> options)
            : base(options)
        {
        }
    }
}
