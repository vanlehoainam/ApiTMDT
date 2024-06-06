using ApiTMDT.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTMDT.Data
{
    public class UserContext : DbContext
    {
        public UserContext( DbContextOptions<UserContext> opt) : base(opt) 
        { 
        }
        public DbSet <UserModel>? Users { get; set; }
    }
}
