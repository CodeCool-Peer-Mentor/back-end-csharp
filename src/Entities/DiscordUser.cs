namespace Codecool.PeerMentors.Entities
{
    public class DiscordUser
    {
        public DiscordUser(DTOs.Requests.DiscordUser user, User owner)
        {
            ID = user.ID;
            Username = user.Username;
            Discriminator = user.Discriminator;
            User = owner;
        }

        private DiscordUser()
        {
        }

        public string ID { get; set; }

        public string Username { get; set; }

        public string Discriminator { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
