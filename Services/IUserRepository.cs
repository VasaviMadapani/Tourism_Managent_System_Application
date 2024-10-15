using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;

namespace Tourism_Management_System_API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserManagement>> GetUsersAsync();
        Task<UserManagement> GetUserByIdAsync(int id);
        Task<UserManagement> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserManagement user);
        Task UpdateUserAsync(UserManagement user);
        Task<bool> UserExistsByEmailAsync(string email);
        Task DeleteUserAsync(UserManagement user);

        Task SaveChangesAsync();
        IEnumerable<UserManagement> SearchUsers(UserSearchDto searchCriteria);
    }
}
