using SharedModels;
using SharedModels.Dto;

namespace PayrolAPI.Repository.IRepository
{
    public interface IIncomeRepository : IRepository<Income>
    {
        Task<Income> UpdateAsync(Income entity);
        Task<bool> AddIncomeAsync(IncomeCreateDto incomeDto);

        Task<double> CalculateTotalIncomeAsync(int employeeId);

    }
}
