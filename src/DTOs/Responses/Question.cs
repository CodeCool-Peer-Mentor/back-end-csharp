namespace Codecool.PeerMentors.DTOs.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class Question
    {
        public Question(Entities.Question entity)
        {
            ID = entity.ID;
            Title = entity.Title;
            Body = entity.Body;
            IsAnonymous = entity.IsAnonymous;
            if (!IsAnonymous)
            {
                AuthorID = entity.Author.Id;
                AuthorName = $"{entity.Author.FirstName} {entity.Author.LastName}";
            }

            Technologies = entity.Technologies.Select(qt => Technology.From(qt.Technology));
            AutheredAt = entity.InsertedAt;
        }

        public int ID { get; }

        public string Title { get; }

        [JsonPropertyName("description")]
        public string Body { get; }

        [JsonPropertyName("userId_")]
        public string AuthorID { get; }

        [JsonPropertyName("username")]
        public string AuthorName { get; }

        [JsonPropertyName("anonym")]
        public bool IsAnonymous { get; }

        [JsonPropertyName("technologyTags")]
        public IEnumerable<Technology> Technologies { get; }

        [JsonPropertyName("submissionTime")]
        public DateTime AutheredAt { get; }
    }
}
