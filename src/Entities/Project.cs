namespace Codecool.PeerMentors.Entities
{
    using System;
    using System.Collections.Generic;

    public class Project : Tag
    {
        public int ID { get; set; }

        public DateTime InsertedAt { get; set; }

        public User AddedBy { get; set; }

        public List<UserProject> Users { get; set; }

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
