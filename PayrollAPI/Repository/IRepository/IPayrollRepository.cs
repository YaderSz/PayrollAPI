using SharedModels;

namespace PayrolAPI.Repository.IRepository
{
    public interface IPayrollRepository : IRepository<Payroll>
    {
        Task<Payroll> UpdateAsync(Payroll entity);

    }
}
