using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using PayrollAPI.Data;
using SharedModels;
using SharedModels.Dto;

namespace PayrolAPI.Repository
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {
        private readonly PayrollContext _context;
        private readonly ICalculosIRepository _calculos;

        public IncomeRepository(PayrollContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Income> UpdateAsync(Income entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> AddIncomeAsync(IncomeCreateDto incomeDto)
        {
            try
            {
                var Ingreso = new Income
                {
                    EmployeeID = incomeDto.EmployeeID,
                    SalarioOrdinario = incomeDto.SalarioOrdinario,
                    RiesgoLaboral = incomeDto.RiesgoLaboral,
                    Antiguedad = _calculos.CalcularAntiguedad(incomeDto.AñosAntiguedad, incomeDto.SalarioOrdinario),
                    Nocturnidad = incomeDto.Nocturnidad,
                    CntdHorasExtras = incomeDto.CntdHorasExtras,
                    SalarioExtraOrdinario = _calculos.CalcularHorasextra(incomeDto.SalarioOrdinario, incomeDto.CntdHorasExtras)
                };

                _context.Incomes.Add(Ingreso);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar ingreso: {ex.Message}");
                return false;
            }
        }
        public async Task<double> CalculateTotalIncomeAsync(int employeeId)
        {
            var totalIncome = await _context.Incomes
                .Where(i => i.EmployeeID == employeeId)
                .SumAsync(i => i.SalarioOrdinario + i.Antiguedad + i.RiesgoLaboral + i.Nocturnidad + i.SalarioExtraOrdinario);

            return totalIncome;
        }
    }
}
