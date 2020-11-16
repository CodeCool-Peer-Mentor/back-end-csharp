namespace Codecool.PeerMentors.DTOs
{
    using System.Text.Json.Serialization;

    public class Technology
    {
        public int ID { get; set; }

        [JsonPropertyName("technologyTag")]
        public string Name { get; set; }

        public static Technology From(Entities.Technology entity)
        {
            return new Technology()
            {
                ID = entity.ID,
                Name = entity.Name,
            };
        }
    }
}
