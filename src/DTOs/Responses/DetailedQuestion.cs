namespace Codecool.PeerMentors.DTOs.Responses
{
    public class DetailedQuestion
    {
        public DetailedQuestion(Question question)
        {
            Question = question;
        }

        public Question Question { get; }

        public string[] Answers { get; set; } = new string[0];
    }
}
