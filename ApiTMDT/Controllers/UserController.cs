using ApiTMDT.Data;
using ApiTMDT.Models;
using ApiTMDT.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IAccountRepository accountRepo;
        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserModel> objCatlist = _context.Users.ToList();
            return Ok(objCatlist);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel empobj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Users.AddAsync(empobj);
            await _context.SaveChangesAsync();
            return Ok(empobj); // Return the created user object
        }

        [HttpPut("{IdUser}")]
        public async Task<IActionResult> Update(int IdUser, [FromBody] UserModel empobj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _context.Users.FindAsync(IdUser);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update user properties
            existingUser.Name = empobj.Name;
            existingUser.Email = empobj.Email;
            // Add other properties as needed

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }
        
        [HttpDelete("{IdUser}")]
        public async Task<IActionResult> Delete(int IdUser)
        {
            var existingUser = await _context.Users.FindAsync(IdUser);
            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
