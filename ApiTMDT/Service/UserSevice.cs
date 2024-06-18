﻿using ApiTMDT.Models;
using Data;
using ApiTMDT.Repositories;
using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using EO.Base;
using Microsoft.AspNet.Identity;
using static ApiTMDT.Service.UserService;

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

        public async Task<(UserModel user, string message)> LoginAsync(string emailorusername, string Password)
        {


            var user = await _context.Users
                .Select(x => new UserModel { Email = x.Email, UserName = x.UserName, Password = x.Password })
                .Where(x => (x.Email == emailorusername || x.UserName == emailorusername) && x.Password == Password)
                .SingleOrDefaultAsync();

            return (user, "Đăng nhập thành công.");
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

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (user, "Tạo user thành công.");
        }

        public async Task<(UserModel user, string message)> UpdateUserAsync(int Id, UserModel userUpdate)
        {
            var existingUser = await _context.Users.FindAsync(Id);
            if (existingUser == null)
            {
                return (null, "User không tồn tại.");
            }

            var userWithSameUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userUpdate.UserName && u.Id != Id);

            if (userWithSameUsername != null)
            {
                return (null, "Username đã tồn tại.");
            }

            var userWithSameEmail = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userUpdate.Email && u.Id != Id);

            if (userWithSameEmail != null)
            {
                return (null, "Email đã tồn tại.");
            }

            existingUser.UserName = userUpdate.UserName;
            existingUser.Email = userUpdate.Email;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return (existingUser, "Cập nhật user thành công.");
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return new DeleteUserResponse
                {
                    Success = false,
                    Message = "Không tìm thấy user"
                };
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return new DeleteUserResponse
            {
                Success = true,
                Message = "Xóa thành công"
            };
        }

        public class DeleteUserResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
