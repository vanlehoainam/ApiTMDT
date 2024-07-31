using ApiTMDT.Models;
using Data;
using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ApiTMDT.Service
{
    public class UserService
    {
        private readonly ApiDbContext _context;

        public UserService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(UserModel user, string message)> LoginAsync(string emailOrUsername, string password)
        {
            if (string.IsNullOrWhiteSpace(emailOrUsername) || string.IsNullOrWhiteSpace(password))
            {
                return (null, "Email/Username và mật khẩu không được để trống.");
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.Email == emailOrUsername || x.UserName == emailOrUsername);

            if (user == null)
            {
                return (null, "Thông tin đăng nhập không chính xác.");
            }

            bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.Password);
            if (!isPasswordValid)
            {
                return (null, "Mật khẩu đăng nhập không chính xác.");
            }

            string roleMessage = user.Role switch
            {
                "Admin" => "Đăng nhập thành công với vai trò Admin.",
                "NhanVien" => "Đăng nhập thành công với vai trò Nhân Viên.",
                "KhachHang" => "Đăng nhập thành công với vai trò Khách Hàng.",
                _ => "Đăng nhập thành công."
            };

            return (user, roleMessage);
        }

        public async Task<(UserModel user, string message)> CreateUserAsync(UserModel user)
        {
            var existingUserByUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == user.UserName);

            if (existingUserByUsername != null)
            {
                return (null, "Username đã tồn tại.");
            }

            var existingUserByEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUserByEmail != null)
            {
                return (null, "Email đã tồn tại.");
            }

            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (user, "Tạo user thành công.");
        }

        public async Task<(UserModel originalUser, UserModel updatedUser, string message)> UpdateUserAsync(int id, UserModel userUpdate)
        {
            // Tìm kiếm người dùng hiện tại
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return (null, null, "User không tồn tại.");
            }

            // Kiểm tra nếu có user với cùng username hoặc email nhưng khác id
            var userWithSameUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userUpdate.UserName && u.Id != id);
            if (userWithSameUsername != null)
            {
                return (null, null, "Username đã tồn tại.");
            }

            var userWithSameEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userUpdate.Email && u.Id != id);
            if (userWithSameEmail != null)
            {
                return (null, null, "Email đã tồn tại.");
            }

            // Lưu trữ thông tin người dùng gốc
            var originalUser = new UserModel
            {
                Id = existingUser.Id,
                FullName = existingUser.FullName,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Role = existingUser.Role,
                Phone = existingUser.Phone,
                Password =  existingUser.Password,
            };

            // Cập nhật thông tin người dùng
            existingUser.FullName = userUpdate.FullName;
            existingUser.UserName = userUpdate.UserName;
            existingUser.Email = userUpdate.Email;
            existingUser.Phone= userUpdate.Phone;
            existingUser.Password= userUpdate.Password;
            existingUser.Role = userUpdate.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return (originalUser, existingUser, "Cập nhật user thành công.");
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return new DeleteUserResponse
                {
                    Success = false,
                    Message = "Không tìm thấy user."
                };
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return new DeleteUserResponse
            {
                Success = true,
                Message = "Xóa user thành công."
            };
        }

        public class DeleteUserResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
