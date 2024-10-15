using Microsoft.AspNetCore.Mvc;
using Tourism_Management_System_API.Authentication;
using Tourism_Management_System_API.DTO;

namespace Tourism_Management_System_API_Project_.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterAsync(UserRegistrationDTO model);
        Task<string> LoginAsync(string username, string password);
    }
}
