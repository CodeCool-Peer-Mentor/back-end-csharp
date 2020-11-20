namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class PublicMentorPage
    {
        public PublicMentorPage(Entities.User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Country = user.Country;
            City = user.City;
            Module = user.Module;
            Projects = user.Projects.Select(up => Project.From(up.Tag));
            Technologies = user.Technologies.Select(ut => Technology.From(ut.Tag));
            DiscordId = user.Discord?.ID;
            DiscordUsername = user.Discord?.Username;
            Discriminator = user.Discord?.Discriminator;
            Email = user.Email;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string Country { get; }

        public string City { get; }

        public string Module { get; }

        [JsonPropertyName("projectTags")]
        public IEnumerable<Project> Projects { get; }

        [JsonPropertyName("technologyTags")]
        public IEnumerable<Technology> Technologies { get; }

        public string DiscordId { get; }

        public string DiscordUsername { get; }

        public string Discriminator { get; }

        public string Email { get; }
    }
}
