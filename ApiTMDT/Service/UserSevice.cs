using ApiTMDT.Models;
using Data;
using ApiTMDT.Repositories;
using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using EO.Base;

namespace ApiTMDT.Service
{
    public class UserService 
    {
        private readonly UserContext _context;

        public UserService(UserContext context)
        {
            _context = context;
        }
        public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
               .ToListAsync();
        }
        public UserModel Authenticate(string email, string password)
        {
            var hashedPassword = PasswordHelper.HashPassword(password);
            var user = _context.Users.SingleOrDefault(x => x.Email == email && x.Password == hashedPassword);

            return user;
        }

        public async Task<(UserModel, string)> CreateUserAsync(UserModel user)
        {
            var isEmailExist = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (isEmailExist)
            {
                throw new BaseException(null, "Email đã tồn tại.");
            }

            // Kiểm tra username trùng lặp
            var isUsernameExist = await _context.Users.AnyAsync(u => u.UserName == user.UserName);
            if (isUsernameExist)
            {
                throw new BaseException(ErrorsMessage.MSG_USERNAME_EXISTS, "Username đã tồn tại.");
            }


            user.Password = PasswordHelper.HashPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return (user, "Người dùng đã được tạo thành công.");
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
