namespace Codecool.PeerMentors.DTOs.Responses
{
    using System;
    using System.Text.Json.Serialization;

    public class MinimalQuestion
    {
        public MinimalQuestion(Entities.Question question)
        {
            ID = question.ID;
            Title = question.Title;
            Body = question.Body;
            AuthoredAt = question.InsertedAt;
        }

        public int ID { get; }

        public string Title { get; }

        [JsonPropertyName("description")]
        public string Body { get; }

        [JsonPropertyName("submissionTime")]
        public DateTime AuthoredAt { get; set; }
    }
}
