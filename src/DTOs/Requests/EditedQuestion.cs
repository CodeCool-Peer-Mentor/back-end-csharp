namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Text.Json.Serialization;

    public class EditedQuestion
    {
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Body { get; set; }
    }
}
