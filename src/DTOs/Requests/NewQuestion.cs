namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class NewQuestion
    {
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Body { get; set; }

        [JsonPropertyName("anonym")]
        public bool IsAnonymous { get; set; }

        [JsonPropertyName("technologyTags")]
        public List<string> Technologies { get; set; }
    }
}
