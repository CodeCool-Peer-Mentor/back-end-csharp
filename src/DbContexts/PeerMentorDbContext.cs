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

        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionTechnology> QuestionTechnologies { get; set; }

        public DbSet<QuestionVote> QuestionVotes { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Review> Reviews { get; set; }

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
                .WithOne(u => u.Tag);
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

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(t => t.User);
            modelBuilder.Entity<Project>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tag);
            modelBuilder.Entity<UserProject>()
                .Property(ut => ut.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .HasMany(user => user.Questions)
                .WithOne(question => question.Author);
            modelBuilder.Entity<Question>()
                .Property(question => question.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Technologies)
                .WithOne(qt => qt.Question);
            modelBuilder.Entity<Technology>()
                .HasMany(t => t.Questions)
                .WithOne(qt => qt.Technology);

            modelBuilder.Entity<User>()
                .HasMany(u => u.QuestionVotes)
                .WithOne(qv => qv.Voter);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Votes)
                .WithOne(qv => qv.Question);
            modelBuilder.Entity<QuestionVote>()
                .Property(qv => qv.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Answers)
                .WithOne(a => a.Author);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question);
            modelBuilder.Entity<Answer>()
                .Property(a => a.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.Reviewer);
            modelBuilder.Entity<Review>()
                .Property(a => a.InsertedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
