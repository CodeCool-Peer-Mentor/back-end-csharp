namespace Codecool.PeerMentors.Models
{
    public class GoogleSettings
    {
        public GoogleSettings(string clientID, string clientSecret)
        {
            ClientID = clientID;
            ClientSecret = clientSecret;
        }

        public string ClientID { get; }

        public string ClientSecret { get; }
    }
}
