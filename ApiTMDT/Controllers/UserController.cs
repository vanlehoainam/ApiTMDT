using ApiTMDT.Helpers;
using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly UserContext _context;


        public UserController(UserService userService, UserContext context)
        {
            _userService = userService;
            _context = context;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserModel user)
        {
            var result = await _userService.CreateUserAsync(user);

            if (result.user == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.user,
                message = result.message
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string emailorusername ,string Password)
        {
           
                var result = await _userService.LoginAsync(emailorusername, Password);
            if (result.user == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.user,
                message = result.message
            }); ;


        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] UserModel user)
        {
            var result = await _userService.UpdateUserAsync(Id, user);

            if (result.user == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.user,
                message = result.message
            });
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _userService.DeleteUserAsync(Id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
    }
}
