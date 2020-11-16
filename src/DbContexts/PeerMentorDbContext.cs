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

        public DbSet<Technology> Techonologies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Technology>()
                .HasIndex(t => t.Name)
                .IsUnique();
            modelBuilder.Entity<Technology>()
                .Property(t => t.Name)
                .IsRequired();
            modelBuilder.Entity<Technology>()
                .Property(t => t.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
