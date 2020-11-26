namespace Codecool.PeerMentors.DTOs.Responses
{
    using System;
    using System.Text.Json.Serialization;

    public class MinimalAnswer
    {
        public MinimalAnswer(Entities.Answer answer)
        {
            QuestionID = answer.Question.ID;
            QuestionTitle = answer.Question.Title;
            Body = answer.Body;
            AuthoredAt = answer.InsertedAt;
        }

        [JsonPropertyName("questionId_")]
        public int QuestionID { get; }

        public string QuestionTitle { get; }

        [JsonPropertyName("content")]
        public string Body { get; }

        [JsonPropertyName("submissionTime")]
        public DateTime AuthoredAt { get; set; }
    }
}
