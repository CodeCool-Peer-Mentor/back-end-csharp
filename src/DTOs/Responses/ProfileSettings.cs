namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Collections.Generic;

    public class ProfileSettings
    {
        public ProfileSettings(Entities.User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Country = user.Country;
            City = user.City;
            Module = user.Module;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Module { get; set; }

        public string DiscordUsername { get; set; }

        public IEnumerable<object> ProjectTags { get; set; } = new List<object>();

        public IEnumerable<Technology> TechnologyTags { get; set; } = new List<Technology>();

        public IEnumerable<object> AllProjectTags { get; set; } = new List<object>();

        public IEnumerable<Technology> AllTechnologyTags { get; set; } = new List<Technology>();
    }
}
