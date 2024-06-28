using Microsoft.EntityFrameworkCore;
using PayrolAPI.Repository.IRepository;
using PayrollAPI.Data;
using SharedModels;
using SharedModels.Dto;


namespace PayrolAPI.Repository
{
    public class DeductionRepository : Repository<Deduction>, IDeductionRepository
    {
        private readonly PayrollContext _context;
        private readonly CalculosRepository _calculos;

        public DeductionRepository(PayrollContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Deduction> UpdateAsync(Deduction entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> AddDeductionAsync(DeductionCreateDto entity)
        {
            double CINSS = _calculos.CalcularINSS(entity.Salario);


            double ir = _calculos.CalcularIR(entity.Salario, CINSS);
            try
            {
                var Deductions = new Deduction
                {
                    EmployeeID = entity.EmployeeID,
                    INSS = CINSS,
                    IR = ir
                };
                _context.Deductions.Add(Deductions);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar egreso: {ex.Message}");
                return false;
            }
        }
        public async Task<double> CalculateTotalDeductionAsync(int employeeId)
        {
            var totalDeductions = await _context.Deductions
                .Where(i => i.EmployeeID == employeeId)
                .SumAsync(i =>  i.INSS + i.IR );

            return totalDeductions;
        }


    }
}
