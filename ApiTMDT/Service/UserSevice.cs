using ApiTMDT.Models;
using Data;
using ApiTMDT.Repositories;
using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ApiTMDT.Service
{
    public class UserService 
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }
        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public UserModel Authenticate(string email, string password)
        {
            var hashedPassword = PasswordHelper.HashPassword(password);
            var user = _context.Users.SingleOrDefault(x => x.Email == email && x.Password == hashedPassword);

            return user;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            user.Password = PasswordHelper.HashPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return false;
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserModel> UpdateUserAsync(int userId, UserModel userUpdate)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserName = userUpdate.UserName;
            existingUser.Email = userUpdate.Email;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return existingUser;
        }

    } 
}
