namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class PrivateUser
    {
        public PrivateUser(Entities.User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Country = user.Country;
            City = user.City;
            Module = user.Module;
            Questions = user.Questions.Select(q => new MinimalQuestion(q));
            Answers = user.Answers.Select(a => new MinimalAnswer(a));
        }

        public string FirstName { get; }

        public string LastName { get; }

        public string Country { get; }

        public string City { get; }

        public string Module { get; }

        [JsonPropertyName("userQuestions")]
        public IEnumerable<MinimalQuestion> Questions { get; }

        [JsonPropertyName("userAnswers")]
        public IEnumerable<MinimalAnswer> Answers { get; }
    }
}
