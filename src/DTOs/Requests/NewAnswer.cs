namespace Codecool.PeerMentors.DTOs.Requests
{
    using System;
    using System.Text.Json.Serialization;

    public class NewAnswer
    {
        [JsonPropertyName("questionId")]
        public string StringQuestionId { get; set; }

        [JsonIgnore]
        public int QuestionID => Convert.ToInt32(StringQuestionId);

        [JsonPropertyName("content")]
        public string Body { get; set; }
    }
}
