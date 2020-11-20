namespace Codecool.PeerMentors.Controllers
{
    using System.Threading.Tasks;
    using Codecool.PeerMentors.DTOs.Requests;
    using Codecool.PeerMentors.Entities;
    using Codecool.PeerMentors.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody] GoogleUser account)
        {
            User user = await authService.Authenticate(account);
            await authService.Authorize(user);
            return Ok();
        }

        [HttpGet("authentication")]
        public IActionResult AuthCheck()
        {
            return Ok();
        }

        [HttpGet("logout")]
        public Task SignOut()
        {
            return authService.SignOut();
        }
    }
}
