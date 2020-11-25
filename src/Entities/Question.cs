namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Question
    {
        public Question(DTOs.Requests.NewQuestion dto, User author)
        {
            Title = dto.Title;
            Body = dto.Body;
            IsAnonymous = dto.IsAnonymous;
            Author = author;
        }

        private Question()
        {
        }

        public int ID { get; private set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(9999, MinimumLength = 3)]
        public string Body { get; set; }

        public bool IsAnonymous { get; private set; }

        [Required]
        public User Author { get; private set; }

        public List<QuestionTechnology> Technologies { get; private set; }

        public List<QuestionVote> Votes { get; private set; }

        public List<Answer> Answers { get; private set; }

        [Required]
        public DateTime InsertedAt { get; private set; }
    }
}
