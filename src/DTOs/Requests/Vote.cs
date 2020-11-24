namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Text.Json.Serialization;

    public class Vote
    {
        [JsonPropertyName("vote")]
        public int Value { get; set; }
    }
}
