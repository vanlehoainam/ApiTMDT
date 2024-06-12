using ApiTMDT.Models;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Data;

namespace ApiTMDT.Repositories
{
    public interface  AccountRepository
    {
        void Authenticate(string email, string password);
        void CreateUserAsync(UserModel user);
        void UpdateUserAsync(int Id );
        void DeleteUserAsync(int Id);
    }

}
