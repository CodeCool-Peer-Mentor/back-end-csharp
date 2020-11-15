namespace Codecool.PeerMentors.Services
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using static Google.Apis.Auth.GoogleJsonWebSignature;

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;

        public AuthService(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Validates a Google-issued Json Web Token (JWT).
        /// </summary>
        /// <param name="gUser">User details from Google.</param>
        /// <returns>The payload of the verified token.</returns>
        public async Task Authenticate(GoogleUser gUser)
        {
            Payload googleUser = await ValidateAsync(gUser.Token, new ValidationSettings());
            User user = await userManager.FindByEmailAsync(googleUser.Email);
            if (user != null)
            {
                return;
            }

            user = new User(googleUser);

            await userManager.CreateAsync(user);
        }
    }
}
