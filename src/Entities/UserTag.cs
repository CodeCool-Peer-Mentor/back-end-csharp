namespace Codecool.PeerMentors.Entities
{
    using System;

    public abstract class UserTag<T>
    {
        protected UserTag()
        {
        }

        public int ID { get; set; }

        public User User { get; set; }

        public T Tag { get; set; }

        public DateTime InsertedAt { get; set; }
    }
}
