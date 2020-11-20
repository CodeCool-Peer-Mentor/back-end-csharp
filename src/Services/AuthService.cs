namespace Codecool.PeerMentors.Services
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.Entities;
    using Microsoft.AspNetCore.Identity;
    using static Google.Apis.Auth.GoogleJsonWebSignature;

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Validates a Google-issued Json Web Token (JWT).
        /// </summary>
        /// <param name="gUser">User details from Google.</param>
        /// <returns>The payload of the verified token.</returns>
        public async Task<User> Authenticate(GoogleUser gUser)
        {
            Payload googleUser = await ValidateAsync(gUser.Token, new ValidationSettings());
            User user = await userManager.FindByEmailAsync(googleUser.Email);
            if (user != null)
            {
                return user;
            }

            user = new User(googleUser);

            IdentityResult result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Couldn't create user");
            }

            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            return user;
        }

        public async Task Authorize(User user)
        {
            User authedUser = await userManager.FindByEmailAsync(user.Email);
            if (authedUser == null)
            {
                throw new NotImplementedException($"User with email [{user.Email}] doesn't exist.");
            }

            await signInManager.SignInAsync(user, isPersistent: true);
        }

        public Task SignOut() => signInManager.SignOutAsync();
    }
}
