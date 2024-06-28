using SharedModels;
using SharedModels.Dto;

namespace PayrolAPI.Repository.IRepository
{
    public interface IDeductionRepository : IRepository<Deduction>
    {
        Task<Deduction> UpdateAsync(Deduction entity);
        Task<bool> AddDeductionAsync(DeductionCreateDto entity);

        Task<double> CalculateTotalDeductionAsync(int employeeId);


    }
}
