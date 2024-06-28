using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using PayrollAPI.Data;
using SharedModels;

namespace PayrolAPI.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly PayrollContext _context;
        private readonly IDeductionRepository _deductionRepository;
        private readonly IIncomeRepository _incomeRepository;

        public EmployeeRepository(IDeductionRepository deductionRepository, IIncomeRepository incomeRepository, PayrollContext context) : base(context)
        {
            _context = context;
            _deductionRepository = deductionRepository;
            _incomeRepository = incomeRepository;
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<double> CalculateNetSalaryAsync(int employeeId)
        {
            try
            {
                // Obtener las deducciones totales del empleado
                double Deducciones = await _deductionRepository.CalculateTotalDeductionAsync(employeeId);

                // Obtener los ingresos totales del empleado
                double Ingresos = await _incomeRepository.CalculateTotalIncomeAsync(employeeId);
                

                // Calcular el salario neto
                double netSalary = Ingresos - Deducciones;

                return netSalary;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular el salario neto para el empleado con ID {employeeId}: {ex.Message}");
                throw;
            }
        }
    }
}
