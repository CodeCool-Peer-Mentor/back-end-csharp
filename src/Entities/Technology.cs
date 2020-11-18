namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.Collections.Generic;
    using DTO = Codecool.PeerMentors.DTOs.Technology;

    public class Technology : Tag
    {
        public int ID { get; set; }

        public DateTime InsertedAt { get; set; }

        public User AddedBy { get; set; }

        public List<UserTechnology> User { get; set; }

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
