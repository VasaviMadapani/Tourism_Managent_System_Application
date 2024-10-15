using Microsoft.EntityFrameworkCore;
using Tourism_Management_System_API.DTO;
using Tourism_Management_System_API.Models;
using Tourism_Management_System_API_Project_.Data;

namespace Tourism_Management_System_API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly TourManagementSystemContext _context;

        public UserRepository(TourManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserManagement>> GetUsersAsync()
        {
            return await _context.UserManagement.ToListAsync();
        }

        public async Task<UserManagement> GetUserByIdAsync(int id)
        {
            return await _context.UserManagement.FindAsync(id);
        }

        public async Task<UserManagement> GetUserByEmailAsync(string email)
        {
            return await _context.UserManagement.SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(UserManagement user)
        {
            await _context.UserManagement.AddAsync(user);
        }

        public async Task UpdateUserAsync(UserManagement user)
        {
            _context.UserManagement.Update(user);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _context.UserManagement.AnyAsync(u => u.Email == email);
        }
        public IEnumerable<UserManagement> SearchUsers(UserSearchDto searchCriteria)
        {
            var query = _context.UserManagement.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.Name))
            {
                query = query.Where(u => u.Username.Contains(searchCriteria.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(searchCriteria.Email))
            {
                query = query.Where(u => u.Email.Contains(searchCriteria.Email, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(searchCriteria.Role))
            {
                query = query.Where(u => u.Role.Contains(searchCriteria.Role, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(UserManagement user)
        {
            _context.UserManagement.Remove(user);  // Remove user from DbContext
            await _context.SaveChangesAsync();  // Save changes to the database
        }



    }
}
