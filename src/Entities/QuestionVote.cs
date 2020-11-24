namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class QuestionVote
    {
        public QuestionVote(User voter, Question question, int value)
        {
            Voter = voter;
            Question = question;
            Value = value;
        }

        private QuestionVote()
        {
        }

        public int ID { get; private set; }

        [Required]
        public User Voter { get; private set; }

        [Required]
        public Question Question { get; private set; }

        public int Value { get; private set; }

        public DateTime InsertedAt { get; private set; }
    }
}
