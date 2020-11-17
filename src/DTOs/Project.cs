namespace Codecool.PeerMentors.DTOs
{
    using System.Text.Json.Serialization;

    public class Project
    {
        public int ID { get; set; }

        [JsonPropertyName("projectTag")]
        public string Name { get; set; }

        public static Project From(Entities.Project entity)
        {
            return new Project
            {
                ID = entity.ID,
                Name = entity.Name,
            };
        }
    }
}
