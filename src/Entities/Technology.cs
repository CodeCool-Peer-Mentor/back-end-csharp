namespace Codecool.PeerMentors.Entities
{
    using System;
    using DTO = Codecool.PeerMentors.DTOs.Technology;

    public class Technology
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime InsertedAt { get; set; }

        public User AddedBy { get; set; }

        public static Technology From(DTO dto, User author)
        {
            return new Technology
            {
                Name = dto.Name,
                AddedBy = author,
            };
        }
    }
}
