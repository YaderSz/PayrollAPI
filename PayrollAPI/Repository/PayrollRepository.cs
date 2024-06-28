using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using PayrollAPI.Data;
using SharedModels;

namespace PayrolAPI.Repository
{
    public class PayrollRepository : Repository<Payroll>, IPayrollRepository
    {
        private readonly PayrollContext _context;

        public PayrollRepository(PayrollContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payroll> UpdateAsync(Payroll entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
