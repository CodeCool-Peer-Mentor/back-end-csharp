namespace Codecool.PeerMentors.DTOs.Requests
{
    using System.Collections.Generic;

    public class MentorSearchQuery
    {
        public List<Project> Projects { get; set; }

        public List<Technology> Technologies { get; set; }
    }
}
