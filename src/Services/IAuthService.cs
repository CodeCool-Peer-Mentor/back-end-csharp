namespace Codecool.PeerMentors.Services
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;

    public interface IAuthService
    {
        Task Authenticate(GoogleUser user);
    }
}
