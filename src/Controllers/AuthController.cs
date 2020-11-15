namespace Codecool.PeerMentors.Controllers
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleUser account)
        {
            await authService.Authenticate(account);
            return Ok();
        }
    }
}
