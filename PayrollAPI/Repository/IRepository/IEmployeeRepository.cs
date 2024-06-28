using SharedModels;

namespace PayrolAPI.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
   
        Task<Employee> UpdateAsync(Employee entity);
        Task<double> CalculateNetSalaryAsync(int employeeId);

    }
}
