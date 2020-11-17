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

        public DbSet<UserTechnology> UserTechonologies { get; set; }

        public DbSet<Project> Projects { get; set; }

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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Technologies)
                .WithOne(t => t.User);
            modelBuilder.Entity<Technology>()
                .HasMany(t => t.User)
                .WithOne(u => u.Technology);
            modelBuilder.Entity<UserTechnology>()
                .Property(ut => ut.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Project>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<Project>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Project>()
                .Property(p => p.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
