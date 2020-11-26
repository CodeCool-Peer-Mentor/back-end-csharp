namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Text.Json.Serialization;

    public class Review
    {
        [JsonPropertyName("rating")]
        public float Numerical { get; set; }

        [JsonPropertyName("review")]
        public string Text { get; set; }

        [JsonPropertyName("reviewedUser")]
        public string RevieweeID { get; set; }
    }
}
