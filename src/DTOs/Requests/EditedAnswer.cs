namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Text.Json.Serialization;

    public class EditedAnswer
    {
        [JsonPropertyName("content")]
        public string Body { get; set; }
    }
}
