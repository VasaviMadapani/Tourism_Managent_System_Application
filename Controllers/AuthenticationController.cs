using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.Authentication;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API_Project_.AuthenticationServices;

namespace Tourism_Management_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Tourism_Management_System_API_Project_.AuthenticationServices.IAuthenticationService _authService;

        public AuthenticationController(Tourism_Management_System_API_Project_.AuthenticationServices.IAuthenticationService authService)
        {
            _authService = authService; // Dependency injection
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO model)
        {
            if (model == null)
            {
                return BadRequest("Invalid registration data.");
            }

            var result = await _authService.RegisterAsync(model); // Calling RegisterAsync
            if (result)
            {
                return Ok(new { Status = "Success", Message = "User registered successfully." });
            }
            return BadRequest(new { Status = "Error", Message = "User registration failed." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid login data.");
            }

            var token = await _authService.LoginAsync(model.Username, model.Password); // Calling LoginAsync
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(new { Token = token }); // Return token
            }
            return Unauthorized(new { Status = "Error", Message = "Invalid credentials." }); // Unauthorized if login fails
        }
    }
}
