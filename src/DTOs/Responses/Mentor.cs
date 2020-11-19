namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class Mentor
    {
        public Mentor(Entities.User user)
        {
            ID = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Technologies = user.Technologies.Select(ut => Technology.From(ut.Tag));
        }

        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonPropertyName("technologyTags")]
        public IEnumerable<Technology> Technologies { get; set; }
    }
}
