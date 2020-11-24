namespace Codecool.PeerMentors.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class QuestionTechnology
    {
        public QuestionTechnology(Question question, Technology technology)
        {
            Question = question;
            Technology = technology;
        }

        private QuestionTechnology()
        {
        }

        public int ID { get; private set; }

        [Required]
        public Question Question { get; }

        [Required]
        public Technology Technology { get; }
    }
}
