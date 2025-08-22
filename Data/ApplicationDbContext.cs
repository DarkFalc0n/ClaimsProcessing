using ClaimsProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimsProcessing.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; } = null!;
    }
}


