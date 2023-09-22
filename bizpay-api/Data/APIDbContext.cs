using bizpay_api.Models;
using Microsoft.EntityFrameworkCore;

namespace bizpay_api.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) {
        
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
