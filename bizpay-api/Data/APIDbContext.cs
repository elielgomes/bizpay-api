using bizpay_api.Repository;
using Microsoft.EntityFrameworkCore;

namespace bizpay_api.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permition> Permitions { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Payslip> Payslips { get; set; }

    }
}
