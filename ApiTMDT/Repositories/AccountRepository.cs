using ApiTMDT.Models;
using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApiTMDT.Data;

namespace ApiTMDT.Repositories
{
    public class AccountRepository
    {
        private readonly UserContext userContext;

        public AccountRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        public UserModel Authenticate(string email, string password)
        {
            var user = userContext.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
            return user;
        }
    }

}
