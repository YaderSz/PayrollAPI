using Microsoft.EntityFrameworkCore;
using SharedModels;

namespace PayrollAPI.Data
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options) :
    base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Income> Incomes { get; set; }  
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<User> Users { get; set; }  




    }
}
