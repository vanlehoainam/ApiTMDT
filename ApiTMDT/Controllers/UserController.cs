using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserModel> users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUserAsync(user);
            return Ok(createdUser);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpPut("{IdUser}")]
        public async Task<IActionResult> Update(int IdUser, [FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUserAsync(IdUser, user);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{IdUser}")]
        public async Task<IActionResult> Delete(int IdUser)
        {
            var success = await _userService.DeleteUserAsync(IdUser);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
