namespace Codecool.PeerMentors.Entities
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using static Google.Apis.Auth.GoogleJsonWebSignature;

    public class User : IdentityUser
    {
        public User(Payload googleUser)
        {
            UserName = googleUser.Email;
            FirstName = googleUser.GivenName;
            LastName = googleUser.FamilyName;
            Email = googleUser.Email;
        }

        private User()
        {
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Module { get; set; }

        public List<UserTechnology> Technologies { get; set; }

        public List<UserProject> Projects { get; set; }
    }
}
