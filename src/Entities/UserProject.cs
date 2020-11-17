namespace Codecool.PeerMentors.Entities
{
    using System;

    public class UserProject
    {
        public UserProject(User user, Project project)
        {
            User = user;
            Project = project;
        }

        private UserProject()
        {
        }

        public int ID { get; set; }

        public User User { get; set; }

        public Project Project { get; set; }

        public DateTime InsertedAt { get; set; }
    }
}
