namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Text.Json.Serialization;

    public class Review
    {
        public Review(Entities.Review review)
        {
            Numerical = review.Numerical;
            Text = review.Text;
            Author = $"{review.Reviewer.FirstName} {review.Reviewer.LastName}";
        }

        [JsonPropertyName("rating")]
        public float Numerical { get; }

        [JsonPropertyName("review")]
        public string Text { get; }

        [JsonPropertyName("reviewer")]
        public string Author { get; }
    }
}
