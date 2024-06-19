using ApiTMDT.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext( DbContextOptions<ApiDbContext> opt) : base(opt) 
        { 
        }
        public DbSet <UserModel> Users { get; set; }
        public DbSet<SanPhamModel> SanPham { get; set; }

    }
}
