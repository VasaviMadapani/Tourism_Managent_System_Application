using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API.Services;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management.Controllers
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Tourism_Management_System_API.DTO;
    using Tourism_Management_System_API.Models;
    using Tourism_Management_System_API.Services;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    namespace Tourism_Management.Controllers
    {
        [Route("api/users")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly IUserRepository _userRepository;
            private readonly TourManagementSystemContext _context;
            private readonly IMapper _mapper;

            public UserController(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registerDto)
            {
                if (await _userRepository.UserExistsByEmailAsync(registerDto.Email))
                {
                    return BadRequest("Email already exists.");
                }

                // Map UserRegistrationDTO to UserManagement using AutoMapper
                var newUser = _mapper.Map<UserManagement>(registerDto);
                await _userRepository.AddUserAsync(newUser);
                await _userRepository.SaveChangesAsync();

                return Ok("User registered successfully.");
            }

            [HttpPost("login")]
           // [Authorize(Roles = "User")]
            public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
            {
                var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

                // Validate user and password (ideally hashed passwords)
                if (user == null || user.Password != loginDto.Password)
                {
                    return Unauthorized("Login failed.");
                }

                return Ok("Login successful.");
            }

            [HttpGet("profile")]
            [Authorize(Roles = "User")]
            public async Task<IActionResult> GetUserProfile([FromQuery] int userId)
            {
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Map user to UserDTO using AutoMapper
                var userDto = _mapper.Map<UserDTO>(user);
                return Ok(userDto);
            }

            [HttpPut("profile")]
            [Authorize(Roles = "User")]
            public async Task<IActionResult> UpdateUserProfile(UserProfileDTO profileDTO)
            {
                var user = await _userRepository.GetUserByIdAsync(profileDTO.UserId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Map UserProfileDTO to UserManagement using AutoMapper
                _mapper.Map(profileDTO, user);

                await _userRepository.UpdateUserAsync(user);
                await _userRepository.SaveChangesAsync();

                return Ok("Profile updated successfully.");
            }

            [HttpPost("logout")]
            [Authorize(Roles = "User")]
            public IActionResult Logout()
            {
                // Clear session or token (implementation depends on your authentication)
                return Ok("Logout successful.");
            }


            [AllowAnonymous]
            [HttpGet]
            //[Authorize(Roles = "User")]
            public async Task<IActionResult> GetUsers()
            {
                var users = await _userRepository.GetUsersAsync();

                // Map users to a list of UserDTO objects using AutoMapper
                var userDtos = _mapper.Map<IEnumerable<UserDTO>>(users);
                return Ok(userDtos);
            }

            [HttpGet("{id}")]
           // [Authorize(Roles = "User")]
            public async Task<IActionResult> GetUserById(int id)
            {
                var user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Map user to UserDTO using AutoMapper
                var userDto = _mapper.Map<UserDTO>(user);
                return Ok(userDto);
            }

            [HttpGet("searchusers")]
            [Authorize(Roles = "User")]
            public async Task<IActionResult> SearchUsers(string name = null, string email = null, string role = null)
            {

                var results = _context.UserManagement.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    results = results.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name));
                if (!string.IsNullOrEmpty(email))
                    results = results.Where(u => u.Email.Contains(email));
                if (!string.IsNullOrEmpty(role))
                    results = results.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));

                return Ok(results.ToList());

            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUser(int id)
            {
                // Fetch the user by ID
                var user = await _userRepository.GetUserByIdAsync(id)
            ;

                // Check if the user exists
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Delete the user using the repository method
                await _userRepository.DeleteUserAsync(user);
                return Ok("User deleted successfully.");
            }
        }
    }

}
