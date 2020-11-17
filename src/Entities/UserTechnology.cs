namespace Codecool.PeerMentors.Entities
{
    using System;

    public class UserTechnology
    {
        public UserTechnology(User user, Technology technology)
        {
            User = user;
            Technology = technology;
        }

        private UserTechnology()
        {
        }

        public int ID { get; set; }

        public User User { get; set; }

        public Technology Technology { get; set; }

        public DateTime InsertedAt { get; set; }
    }
}
