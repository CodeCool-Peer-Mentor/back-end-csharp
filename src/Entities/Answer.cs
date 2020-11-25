namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        public Answer(string body, Question question, User author)
        {
            Body = body;
            Question = question;
            Author = author;
        }

        private Answer()
        {
        }

        public int ID { get; private set; }

        [Required]
        [StringLength(9999, MinimumLength = 2)]
        public string Body { get; private set; }

        [Required]
        public User Author { get; private set; }

        [Required]
        public Question Question { get; private set; }

        public DateTime InsertedAt { get; private set; }
    }
}
