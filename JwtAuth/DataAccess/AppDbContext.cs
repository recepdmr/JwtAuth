using JwtAuth.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.DataAccess
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext) { }

    }
}
