using ApiTMDT.Models;
using Microsoft.AspNetCore.Identity;
namespace ApiTMDT.Repositories
{
    public class IAccountRepository
    {
        public Task<string> SignInAsync(LoginModel model);
    }
}

