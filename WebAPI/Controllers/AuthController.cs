using Business.Abstracts;
using Business.Dtos.Requests.Applicant;
using Microsoft.AspNetCore.Mvc;
using Business.Dtos.Requests.Login;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateApplicantRequest request, [FromQuery] string password)
        {
            var applicant = await _authService.RegisterAsync(request, password);
            var token = _authService.CreateAccessToken(applicant);
            return Ok(new { applicant, token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var applicant = await _authService.LoginAsync(request);
            var token = _authService.CreateAccessToken(applicant);
            return Ok(new { applicant, token });
        }
    }
}
