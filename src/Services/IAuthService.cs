namespace Codecool.PeerMentors.Services
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.Entities;

    public interface IAuthService
    {
        Task<User> Authenticate(GoogleUser user);

        Task Authorize(User user);
    }
}
