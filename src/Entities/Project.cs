namespace Codecool.PeerMentors.Entities
{
    using System;

    public class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime InsertedAt { get; set; }

        public User AddedBy { get; set; }

        public static Project From(DTOs.Project dto, User author)
        {
            return new Project
            {
                Name = dto.Name,
                AddedBy = author,
            };
        }
    }
}
