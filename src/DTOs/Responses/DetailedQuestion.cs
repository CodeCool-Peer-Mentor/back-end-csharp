namespace Codecool.PeerMentors.DTOs.Responses
{
    using System.Collections.Generic;

    public class DetailedQuestion
    {
        public DetailedQuestion(Question question, IEnumerable<Answer> answers)
        {
            Question = question;
            Answers = answers;
        }

        public Question Question { get; }

        public IEnumerable<Answer> Answers { get; }
    }
}
