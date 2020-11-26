namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public Review(float numerical, string text, User reviewer, User reviewee)
        {
            Numerical = numerical;
            Text = text;
            Reviewer = reviewer;
            Reviewee = reviewee;
        }

        private Review()
        {
        }

        public int ID { get; private set; }

        [Required]
        public float Numerical { get; private set; }

        public string Text { get; private set; }

        [Required]
        public User Reviewer { get; private set; }

        [Required]
        public User Reviewee { get; private set; }

        public DateTime InsertedAt { get; private set; }
    }
}
