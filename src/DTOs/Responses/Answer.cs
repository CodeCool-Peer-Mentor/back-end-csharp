namespace Codecool.PeerMentors.DTOs.Responses
{
    using System;
    using System.Text.Json.Serialization;

    public class Answer
    {
        public Answer(Entities.Answer answer)
        {
            ID = answer.ID;
            Body = answer.Body;
            UserID = answer.Author.Id;
            Username = $"{answer.Author.FirstName} {answer.Author.LastName}";
            AuthoredAt = answer.InsertedAt;
        }

        public int ID { get; }

        [JsonPropertyName("content")]
        public string Body { get; }

        [JsonPropertyName("myAnswer")]
        public bool IsMyAnswer { get; set; }

        [JsonPropertyName("userId_")]
        public string UserID { get; }

        public string Username { get; }

        [JsonPropertyName("submissionTime")]
        public DateTime AuthoredAt { get; }
    }
}
